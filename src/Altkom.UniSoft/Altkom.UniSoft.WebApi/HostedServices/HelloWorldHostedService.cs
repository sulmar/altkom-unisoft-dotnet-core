using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.HostedServices
{
    public class HelloWorldHostedService : IHostedService
    {
        private readonly ILogger<HelloWorldHostedService> logger;

        private Timer timer;

        public HelloWorldHostedService(ILogger<HelloWorldHostedService> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, 0, 1000);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            logger.LogInformation($"Hello World!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
