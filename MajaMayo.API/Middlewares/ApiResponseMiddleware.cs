using MajaMayo.API.Models;
using System.Text.Json;
using System.Text;

namespace MajaMayo.API.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);
                responseBody.Seek(0, SeekOrigin.Begin);

                var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

                using (var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(responseBodyText)))
                {
                    
                    var responseObject = await System.Text.Json.JsonSerializer.DeserializeAsync<object>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    string message = null;
                    if (context.Items.ContainsKey("HttpClientResponse"))
                    {
                        var httpClientResponse = context.Items["HttpClientResponse"] as HttpResponseMessage;
                        if (!httpClientResponse.IsSuccessStatusCode)
                        {
                            message = httpClientResponse.ReasonPhrase;
                        }
                    }
                    var apiResponse = new ApiResponse
                    {
                        Data = responseObject,
                        IsSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                        StatusCode = context.Response.StatusCode,
                        Message = message is null ?  "No message" : message
                    };

                    var jsonResponse = System.Text.Json.JsonSerializer.Serialize(apiResponse);

                    context.Response.Body = originalBodyStream;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(jsonResponse);
                }
            }
        }
    }
}

