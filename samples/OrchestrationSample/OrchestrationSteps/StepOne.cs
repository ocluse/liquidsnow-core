using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Orchestrations;

namespace OrchestrationSample.OrchestrationSteps
{

    internal class StepOne : IOrchestrationStep<SampleOrchestration, int>
    {
        public int Order => 1;

        private readonly ILogger<StepOne> _logger;

        public StepOne(ILogger<StepOne> logger)
        {
            _logger = logger;
        }

        public Task<IOrchestrationStepResult> Execute(IOrchestrationData<SampleOrchestration> data, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Performing step 1");
            return Task.FromResult(OrchestrationStepResult.Success());
        }
    }
}
