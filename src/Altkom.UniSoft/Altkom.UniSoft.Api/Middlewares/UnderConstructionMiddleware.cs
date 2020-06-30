using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Altkom.UniSoft.Api.Middlewares
{
    public class UnderConstructionMiddleware
    {
        public UnderConstructionMiddleware(RequestDelegate next) { }
        public Task Invoke(HttpContext context)
        {
            return context.Response.WriteAsync("Under Construction");
        }
    }

    public static class UnderConstructionExtensions
    {
        public static IApplicationBuilder UseUnderConstruction(this IApplicationBuilder app)
        {
           return app.UseMiddleware<UnderConstructionMiddleware>();
        }

    }
}
