using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusSample
{
    internal class DelayedTestEventHandler : IEventHandler<TestEvent>
    {
        private readonly ILogger<DelayedTestEventHandler> _logger;
        public DelayedTestEventHandler(ILogger<DelayedTestEventHandler> logger)
            {
            _logger = logger;
        }
        
        public async Task Handle(TestEvent ev, CancellationToken cancellationToken = default)
        {
            await Task.Delay(5000, cancellationToken);
            _logger.LogInformation($"DELAYED: {ev.Message}");
        }
    }
}
