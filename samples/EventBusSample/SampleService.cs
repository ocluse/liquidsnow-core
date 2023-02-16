using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventBusSample
{
    internal class SampleService : IHostedService
    {
        private readonly ILogger<SampleService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public SampleService(ILogger<SampleService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Run();
            return Task.CompletedTask;
        }

        private async void Run()
        {
            while (true)
            {
                using var scope = _serviceProvider.CreateScope();
                var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
                _logger.LogInformation("Publishing Message");
                await eventBus.Publish(new TestEvent { Message = Guid.NewGuid().ToString(), Timestamp = DateTime.UtcNow });
                _logger.LogInformation("Finished Publishing Message");

                await Task.Delay(10000);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopped SampleService");
            return Task.CompletedTask;
        }
    }
}
