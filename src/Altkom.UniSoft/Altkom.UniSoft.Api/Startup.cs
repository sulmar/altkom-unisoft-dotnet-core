using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Api.Middlewares;
using Altkom.UniSoft.FakeServices;
using Altkom.UniSoft.FakeServices.Fakers;
using Altkom.UniSoft.Models;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Altkom.UniSoft.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();

            services.Configure<FakeCustomerServiceOptions>(Configuration.GetSection("FakeCustomerService"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Middleware
            //app.Use(async (context, next) =>
            //{
            //    Trace.WriteLine($"Logger1 {context.Request.Method} {context.Request.Path}");

            //    await next();

            //    Trace.WriteLine($"{context.Response.StatusCode}");

            //});



            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.ContainsKey("Auhorization"))
            //    {
            //        await next();
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = 401;
            //    }

            //});

            
            // app.UseUnderConstruction();

            // app.UseMiddleware<UnderConstructionMiddleware>();
            app.UseMiddleware<LoggerMiddleware>();
            app.UseMiddleware<AuthorizationMiddleware>();
            
            app.UseMiddleware<ProductsMiddleware>();

            //app.Map("/dashboard", 
            //    options => options.Run(
            //        context => context.Response.WriteAsync("Hello Dasboard")));

            // GET /sensors
            // GET /senors/temp
            // GET /senors/humidity

            app.Map("/sensors", node =>
            {
                node.Map("/temp", options => options.Run(context => context.Response.WriteAsync("26st C")));
                node.Map("/humidity", options => options.Run(context => context.Response.WriteAsync("55%")));

                // default:
                node.Map(string.Empty, options => options.Run(context => context.Response.WriteAsync("26st C 55%")));
            });


            // Endpoints
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/dashboard", async context => await context.Response.WriteAsync("Hello Dashboard"));

                endpoints.Map("/api/products",
                    endpoints.CreateApplicationBuilder()
                        .UseMiddleware<ProductsMiddleware>().Build()
                        );

                endpoints.Map("/api/customers/{id:int}", async context =>
                {
                    int customerId = Convert.ToInt32(context.Request.RouteValues["id"]);

                    ICustomerService customerService = context.RequestServices.GetRequiredService<ICustomerService>();
                    Customer customer = customerService.Get(customerId);

                    string json = JsonSerializer.Serialize(customer);

                    context.Response.Headers.Add("Content-Type", "application/json");
                    
                    await context.Response.WriteAsync(json);
                });
            });
      
      

            app.Run(context => context.Response.WriteAsync("Hello World!"));
            
        }
    }
}
