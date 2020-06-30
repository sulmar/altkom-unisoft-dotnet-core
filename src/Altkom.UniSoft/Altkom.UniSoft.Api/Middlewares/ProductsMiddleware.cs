using Alktom.UniSoft.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Altkom.UniSoft.Api.Middlewares
{
    public class ProductsMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ICustomerService customerService;

        public ProductsMiddleware(RequestDelegate next, ICustomerService customerService)
        {
            this.next = next;
            this.customerService = customerService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/api/customers")
            {
                var customers = customerService.Get();
                string json = JsonSerializer.Serialize(customers);

                context.Response.StatusCode = (int) HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            else
                await next(context);
        }
    }
}
