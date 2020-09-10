using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kaskeset.Server.Runners;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kaskeset.Server
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private BasicRunner _runner;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _runner = new BasicRunner("10.1.0.14", 9000, _logger);
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _runner.Dispose();
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(()=> _runner.Run(),stoppingToken);
        }
    }
}
