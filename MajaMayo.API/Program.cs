using MajaMayo.API.Middlewares;
using MajaMayo.API.Models;
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

builder.Services.AddScoped<IDbConnection>(o => 
new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

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

//Console.WriteLine(builder.Configuration.GetSection("SecurityKey").Value!);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("SecurityKey").Value!);
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


