using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alktom.UniSoft.IServices;
using Altkom.UniSoft.FakeServices;
using Altkom.UniSoft.FakeServices.Fakers;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.WebApi.Constraints;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Altkom.UniSoft.WebApi
{
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
            services.AddControllers();

            services.AddRouting(options =>
            {
                options.ConstraintMap.Add("pesel", typeof(PeselConstraint));
            });

            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
         
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
