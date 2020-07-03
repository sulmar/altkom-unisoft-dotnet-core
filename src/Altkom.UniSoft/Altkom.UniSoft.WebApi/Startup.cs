using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alktom.UniSoft.IServices;
using Altkom.UniSoft.DbServices;
using Altkom.UniSoft.FakeServices;
using Altkom.UniSoft.FakeServices.Fakers;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.Validators;
using Altkom.UniSoft.WebApi.Constraints;
using Altkom.UniSoft.WebApi.HealthChecks;
using Altkom.UniSoft.WebApi.HostedServices;
using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Altkom.UniSoft.WebApi
{
    public static class FakeServicExtensions
    {
        public static IServiceCollection AddFakeServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<IProductService, FakeProductService>();
            services.Configure<FakeCustomerServiceOptions>(configuration.GetSection("FakeCustomerService"));

            return services;
        }
    }

    // dotnet add package NSwag.AspNetCore

    // dotnet add package AspNetCore.HealthChecks.UI
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // OpenAPI
            services.AddOpenApiDocument();

            // dotnet add package FluentValidation.AspNetCore

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<CustomerValidator>()) // auto
                .AddXmlSerializerFormatters();

            // manual
            // services.AddTransient<IValidator<Customer>, CustomerValidator>();

            services.AddRouting(options =>
            {
                options.ConstraintMap.Add("pesel", typeof(PeselConstraint));
            });


            // services.AddFakeServices(Configuration);

            services.AddScoped<ICustomerService, DbCustomerService>();

            string connectionString = Configuration.GetConnectionString("MyConnection");
            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            
            //services.AddDbContext<UniSoftContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContextPool<UniSoftContext>(options => options.UseSqlServer(connectionString));

            // TODO:
            // IOptions<FakeCustomerServiceOptions> options = Options.Create(new FakeCustomerServiceOptions { Count = 40 });            

            services.AddSingleton<ISenderService, FakeEmailSenderService>();

            // dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddHostedService<HelloWorldHostedService>();

            services.AddHealthChecks()                    
                        .AddCheck<RandomHealthCheck>("random");

            services.AddHealthChecksUI();

        }


        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string uri = Configuration["Uri"];

            int limit = int.Parse(Configuration["SmsApi:limit"]);

            string googleMapApiKey = Configuration["GoogleMapsApiKey"];

            // int count = int.Parse(Configuration["FakeCustomerService:Count"]);

            if (env.IsEnvironment("StagingA"))
            {

            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();            

            app.UseAuthorization();


            // OpenApi
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health");

                endpoints.MapHealthChecksUI(options => options.UIPath = "/health-ui");
            });
        }
    }
}
