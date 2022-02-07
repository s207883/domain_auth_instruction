using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MyApp.Middlewares
{
    public class AuthTestMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthTestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var identity = httpContext.User?.Identity;
            if (identity != null)
            {
                var user = new
                {
                    identity.Name,
                    identity.IsAuthenticated,
                    identity.AuthenticationType,
                };

                Console.WriteLine(JsonSerializer.Serialize(user));
            }
            else
            {
                Console.WriteLine("Empty user context.");
            }

            await _next.Invoke(httpContext);
        }
    }
}
