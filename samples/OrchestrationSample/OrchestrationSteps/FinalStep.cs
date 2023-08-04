using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Orchestrations;

namespace OrchestrationSample.OrchestrationSteps
{
    internal class FinalStep : IFinalOrchestrationStep<SampleOrchestration, int>
    {
        private readonly ILogger<FinalStep> _logger;

        public FinalStep(ILogger<FinalStep> logger)
        {
            _logger = logger;
        }

        public Task<int> Execute(IOrchestrationData<SampleOrchestration> data, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Performing final step");

            return Task.FromResult(0);
        }
    }
}
