using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusSample
{
    internal class ImmediateTestEventHandler : IEventHandler<TestEvent>
    {
        private readonly ILogger<ImmediateTestEventHandler> _logger;
        public ImmediateTestEventHandler(ILogger<ImmediateTestEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TestEvent ev, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"IMMEDIATE: {ev.Message}");
            return Task.CompletedTask;
        }
    }
}
