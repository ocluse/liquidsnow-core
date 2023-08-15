using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample
{
    internal class PreSampleCommandExecutionHandler : IPreCommandExecutionHandler<SampleCommand>
    {
        private readonly ILogger<PreSampleCommandExecutionHandler> _logger;

        public PreSampleCommandExecutionHandler(ILogger<PreSampleCommandExecutionHandler> logger)
        {
            _logger = logger;
        }

        public Task<PreExecutionResult> Handle(SampleCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Pre-handled command: {message}", command.Message);

            PreExecutionResult result = new Random().Next(0, 2) == 0
                ? PreExecutionResult.Continue()
                : PreExecutionResult.Stop(Unit.Value);

            _logger.LogInformation("Pre-handled command result: {result}", result);

            return Task.FromResult(result);
        }
    }
}
