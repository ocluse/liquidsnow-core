using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample
{
    internal class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly ILogger<SampleCommandHandler> _logger;

        public SampleCommandHandler(ILogger<SampleCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(SampleCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Handled command: {message}", command.Message);
            return Task.FromResult(Unit.Value);
        }
    }
}
