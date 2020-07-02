using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Altkom.UniSoft.WebApi
{
    // dotnet add package Serilog.AspNetCore

    // dotnet add package Serilog.Enrichers.Thread


    public static class EFCoreExtensions
    {
        public static IHost CreateDatabase<TContext>(this IHost host)
            where TContext : DbContext
        {
            var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<TContext>();

            context.Database.EnsureCreated();

            return host;
        }
    }
}
