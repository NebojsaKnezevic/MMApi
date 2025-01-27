namespace MajaMayo.API.Middlewares
{
   
        public class CookieMiddleware
        {
            private readonly RequestDelegate _next;

            public CookieMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("/Survey", StringComparison.OrdinalIgnoreCase))
                {
                    if (context.Request.Cookies.TryGetValue("ACTTokenAuth", out var token))
                    {
                        context.Request.Headers["Authorization"] = $"Bearer {token}";
                    }
                }

                await _next(context);
            }
        }
    
}
