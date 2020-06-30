using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.Api.Middlewares
{

    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;

        public LoggerMiddleware(RequestDelegate next) // <- wymagane
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            
            Trace.WriteLine($"Logger Middleware {context.Request.Method} {context.Request.Path}");

            await next(context);

            Trace.WriteLine($"{context.Response.StatusCode}");
        }
    }
}
