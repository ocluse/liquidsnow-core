using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Orchestrations;

namespace OrchestrationSample.OrchestrationSteps
{
    internal class StepTwo : IOrchestrationStep<SampleOrchestration, int>
    {
        public int Order => 2;

        private readonly ILogger<StepTwo> _logger;

        public StepTwo(ILogger<StepTwo> logger)
        {
            _logger = logger;
        }

        public Task<OrchestrationStepResult> Execute(IOrchestrationData<SampleOrchestration> data, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Performing step 2");
            return Task.FromResult(OrchestrationStepResult.Success());
        }
    }
}
