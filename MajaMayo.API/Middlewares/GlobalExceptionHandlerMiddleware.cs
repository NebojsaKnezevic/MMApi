using MajaMayo.API.Constants;
using MajaMayo.API.Errors;
using MajaMayo.API.Helpers;
using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using System.Data.SqlClient;
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
            var origin = context.Request.Headers["Origin"];
            //Ispravka cors-a response-a koji dolazi od ratelimiter-a. 
            if (!context.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
            {
                if (origin == "http://localhost:3000" || origin == "https://act.actrs.rs" || origin == "https://www.act.actrs.rs")
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", origin);
                }
                //context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                //context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000 , https://act.actrs.rs");

            }
            //Ispravka cors-a response-a koji dolazi od ratelimiter-a. 
            if (!context.Response.Headers.ContainsKey("Access-Control-Allow-Credentials"))
            {
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            }

            context.Response.ContentType = "application/json";

            var jwtToken = context.Request.Cookies[JWTHelper.SecretTokenName];
            //var userId = JWTHelper.DeconstructJWT(jwtToken)?.Id;
            int? userId = 0;
            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("Survey") && jwtToken is not null)
            {
                userId = JWTHelper.DeconstructJWT(jwtToken)?.Id;
            }

            context.Response.StatusCode = HandleStatusCode(exception);
            //await context.Response.WriteAsync("Unauthorized access");
            var response = new Models.LogEntry
            {
                LogLevel = "Error",
                Message = exception.Message,
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

    private int HandleStatusCode(Exception exception)
    {   
        switch (exception)
        {
            case UnauthorizedAccessException _:
                return (int)HttpStatusCode.Unauthorized;

            //case ForbiddenAccessException _:
            //    return (int)HttpStatusCode.Forbidden;

            case ArgumentException _:
                return (int)HttpStatusCode.BadRequest;

            case KeyNotFoundException _:
                return (int)HttpStatusCode.NotFound;

            case InvalidOperationException _:
                return (int)HttpStatusCode.BadRequest;

            case SqlException _:
                return (int)HttpStatusCode.InternalServerError;

            case TimeoutException _:
                return (int)HttpStatusCode.RequestTimeout;

            case RateLimitExceededException _:
                return (int)HttpStatusCode.TooManyRequests;

            case Exception _:
                return (int)HttpStatusCode.InternalServerError;

            default:
                return (int)HttpStatusCode.InternalServerError;
        }
    }



}
