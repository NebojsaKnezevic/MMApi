using MajaMayo.API.Helpers;
using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using System.Net;
using System.Text.Json;

namespace MajaMayo.API.Middlewares;

public sealed class GlobalExceptionHandlerMiddleware : IMiddleware
{
    
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly ICommandRepository _commandRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger, ICommandRepository commandRepository)
    {
        _logger = logger;
        _commandRepository = commandRepository;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles the specified <see cref="Exception"/> for the specified <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="context">The HTTP httpContext.</param>
    /// <param name="exception">The exception.</param>
    /// <returns>The HTTP response that is modified based on the exception.</returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        

        if (!context.Response.HasStarted)
        {
            context.Response.ContentType = "application/json";

            var jwtToken = context.Request.Cookies[JWTHelper.SecretTokenName];
            var userId = JWTHelper.DeconstructJWT(jwtToken)?.Id;

            if (exception.GetType() == typeof(UnauthorizedAccessException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //await context.Response.WriteAsync("Unauthorized access");
                var response = new Models.LogEntry
                {
                    LogLevel = "Error",
                    Message = "Unauthorized access attempt",
                    Exception = exception.ToString(),
                    EventId = null,
                    Source = "GlobalExceptionHandlerMiddleware",
                    RequestPath = context.Request.Path,
                    UserId = userId
                };

                context.Response.ContentType = "application/json";
                var jsonResponse =  JsonSerializer.Serialize(response);
                if (!context.Response.HasStarted)
                {
                    await context.Response.WriteAsync(jsonResponse);
                }

                await _commandRepository.LogError(response);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = new Models.LogEntry
                {
                    LogLevel = "Error",
                    Message = "Unauthorized access attempt",
                    Exception = exception.ToString(),
                    EventId = null,
                    Source = "GlobalExceptionHandlerMiddleware",
                    RequestPath = context.Request.Path,
                    UserId = userId
                };

                context.Response.ContentType = "application/json";
                var jsonResponse = JsonSerializer.Serialize(response);
                if (!context.Response.HasStarted)
                {
                    await context.Response.WriteAsync(jsonResponse);
                }

                await _commandRepository.LogError(response);
            }
        }

    }


}
