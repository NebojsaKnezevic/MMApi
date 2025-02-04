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

                var contentType = context.Response.ContentType;

                var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

                object responseObject = null;
                string jsonResponse = null;
                string message = null;

                if (contentType != null && contentType.Contains("application/json"))
                {
                    using (var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(responseBodyText)))
                    {
                        responseObject = await JsonSerializer.DeserializeAsync<object>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                }

                if (context.Items.ContainsKey("HttpClientResponse"))
                {
                    var httpClientResponse = context.Items["HttpClientResponse"] as HttpResponseMessage;
                    if (httpClientResponse != null && !httpClientResponse.IsSuccessStatusCode)
                    {
                        message = httpClientResponse.ReasonPhrase;
                    }
                }

                if (contentType != null && contentType.Contains("application/json"))
                {
                    var apiResponse = new ApiResponse
                    {
                        Data = responseObject,
                        IsSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                        StatusCode = context.Response.StatusCode,
                        Message = message ?? "No message"
                    };

                    jsonResponse = JsonSerializer.Serialize(apiResponse);
                }
                else
                {
                    var apiResponse = new ApiResponse
                    {
                        Data = responseBodyText,
                        IsSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                        StatusCode = context.Response.StatusCode,
                        Message = message ?? "No message"
                    };
                    jsonResponse = JsonSerializer.Serialize(apiResponse);
                    //jsonResponse = responseBodyText;
                }

                context.Response.Body = originalBodyStream;

                if (jsonResponse == responseBodyText)
                {
                    context.Response.ContentType = contentType; 
                }
                else
                {
                    context.Response.ContentType = "application/json"; 
                }

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}

