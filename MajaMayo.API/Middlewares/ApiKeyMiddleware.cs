﻿
using Microsoft.Extensions.Configuration;

namespace MajaMayo.API.Middlewares
{
    public class ApiKeyMiddleware : IMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly string ValidApiKey = string.Empty;
        

        public ApiKeyMiddleware(IConfiguration configuration)
        {
            _configuration = configuration;
            ValidApiKey = _configuration.GetValue<string>("ApiKey");
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("DeltaGenerali/RegisterDGUsers"))
            {
                if (!context.Request.Headers.TryGetValue("API_KEY", out var extractedApiKey))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("API Key is missing.");
                    return;
                }

                if (!ValidApiKey.Equals(extractedApiKey))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid API Key.");
                    return;
                }
            }

            await next(context);

        }
    }
}
