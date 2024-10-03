using MajaMayo.API.ConfigModel;
using MajaMayo.API.Middlewares;
using MajaMayo.API.Repository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Logging.ClearProviders(); // Clear default providers if you only want specific providers
builder.Logging.AddConsole(); // Add console logging
builder.Logging.SetMinimumLevel(LogLevel.Error);

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
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://act.actrs.rs", "https://www.act.actrs.rs")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
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
});

var app = builder.Build();

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
app.Run();


