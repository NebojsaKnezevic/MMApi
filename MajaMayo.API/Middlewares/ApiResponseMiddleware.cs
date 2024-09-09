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

                // Read the response body
                var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

                object responseObject = null;
                string jsonResponse = null;
                string message = null;

                if (contentType != null && contentType.Contains("application/json"))
                {
                    using (var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(responseBodyText)))
                    {
                        // Attempt to deserialize JSON content
                        responseObject = await JsonSerializer.DeserializeAsync<object>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                }

                // Check for an HTTP client response to determine the message
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
                    // Wrap JSON response into the ApiResponse object
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
                    // For non-JSON responses, keep the original response body as is
                    jsonResponse = responseBodyText;
                }

                // Write the modified response back to the original stream
                context.Response.Body = originalBodyStream;

                // Set the appropriate content type
                if (jsonResponse == responseBodyText)
                {
                    context.Response.ContentType = contentType; // Keep the original content type
                }
                else
                {
                    context.Response.ContentType = "application/json"; // Set content type to JSON
                }

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}

