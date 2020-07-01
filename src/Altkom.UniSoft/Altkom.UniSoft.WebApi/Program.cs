using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace Altkom.UniSoft.WebApi
{
    // dotnet add package Serilog.AspNetCore

    // dotnet add package Serilog.Enrichers.Thread

    public class Program
    {
        public static void Main(string[] args)
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File(new CompactJsonFormatter(), "logs/log.json")
                .CreateLogger();

            try
            {
                Log.Information($"Application starting in environment: {environmentName}...");

                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Fatal error");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config)=>
                {
                    string environmentName = hostingContext.HostingEnvironment.EnvironmentName;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true);
                    config.AddIniFile("appsettings.ini", optional: true, reloadOnChange: true);
                    config.AddIniFile("appsettings.local.ini", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
                    config.AddUserSecrets(Assembly.GetExecutingAssembly());
                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);
                    
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
