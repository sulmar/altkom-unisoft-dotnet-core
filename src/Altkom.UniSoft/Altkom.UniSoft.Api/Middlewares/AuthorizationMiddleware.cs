using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Altkom.UniSoft.Api.Middlewares
{
    // interfejs IMiddleware

    public class AuthorizationMiddleware 
    {
        private readonly RequestDelegate next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                await next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
