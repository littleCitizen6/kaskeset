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

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            BasicRunner runner = new BasicRunner("10.1.0.14", 9000, _logger);
            /*while (!stoppingToken.IsCancellationRequested)
            {
                
            }*/
        }
    }
}
