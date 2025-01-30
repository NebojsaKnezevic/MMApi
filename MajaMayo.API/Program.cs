using MajaMayo.API.ConfigModel;
using MajaMayo.API.Middlewares;
using MajaMayo.API.Repository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Data;
using System.Data.SqlClient;
using System.Text;




var   builder = WebApplication.CreateBuilder(args);

//var logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.MSSqlServer(
//        connectionString: "YourDatabaseConnectionString",
//        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true })
//    .CreateLogger();

builder.Services.AddLogging();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
builder.Services.AddTransient<ApiKeyMiddleware>();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());


builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IDbConnection>(o => 
    new SqlConnection(connectionString)
    );

builder.Services.Configure<SecuritySettings>(options =>
{
    var section = builder.Configuration.GetSection(SecuritySettings.Name);
    section.Bind(options);

    var securityKey = Environment.GetEnvironmentVariable("SECURITY_KEY");
    if (!string.IsNullOrEmpty(securityKey))
    {
        options.SecurityKey = securityKey;
    }
});


builder.Services.Configure<EmailSettings>(options =>
{
    var section = builder.Configuration.GetSection(EmailSettings.Name);
    section.Bind(options);


    var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
    if (!string.IsNullOrEmpty(smtpPassword))
    {
        options.Password = smtpPassword;
    }
});


builder.Services.AddScoped<IQueryRepository, QueryRepository>();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddScoped<IDGCommandRepository, DGCommandRepository>();

builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:3000", "https://act.actrs.rs", "https://www.act.actrs.rs", "https://accounts.google.com")

                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  //.AllowAnyOrigin()
                  .AllowCredentials();
        });
});

var securityKey = Environment.GetEnvironmentVariable("SECURITY_KEY") ?? builder.Configuration.GetSection(SecuritySettings.Name + ":SecurityKey").Value!;

var key = Encoding.ASCII.GetBytes(securityKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x => {
    x.RequireHttpsMetadata = true;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        //IssuerSigningKey = new SymmetricSecurityKey(
        //    Encoding.UTF8
        //    .GetBytes(configuration["ApplicationSettings:JWT_Secret"])
        //),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.FromMinutes(5)
    };
})
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId =  builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? builder.Configuration["Authentication:Google:ClientSecret"];
})
; 



var app = builder.Build(); 
  

//dsadasdasasdas
//app.Use(async (context, next) =>
//{
//    context.Response.Headers["Cross-Origin-Opener-Policy"] = "unsafe-none";
//    //context.Response.Headers.Remove("Cross-Origin-Opener-Policy");

//    context.Response.Headers["Cross-Origin-Embedder-Policy"] = "unsafe-none";
//    await next();

//});

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;

    options.MessageTemplate = "Executed by {UserName} - " + options.MessageTemplate;
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("UserName", httpContext.User?.Identity?.Name ?? "");
    };
});


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseMiddleware<CookieMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ApiResponseMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.Run();


