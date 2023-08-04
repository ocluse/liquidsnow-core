using Microsoft.Extensions.Logging;
using Ocluse.LiquidSnow.Core.Orchestrations;

namespace OrchestrationSample.OrchestrationSteps
{
    internal class StepThree : IConditionalOrchestrationStep<SampleOrchestration, int>
    {
        public int Order => 3;

        private readonly ILogger<StepThree> _logger;

        public StepThree(ILogger<StepThree> logger)
        {
            _logger = logger;
        }

        public Task<IOrchestrationStepResult> Execute(IOrchestrationData<SampleOrchestration> data, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Performing step 3");
            return Task.FromResult(OrchestrationStepResult.Success());
        }

        public Task<bool> CanExecute(IOrchestrationData<SampleOrchestration> data, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Checking if can execute step 3");

            //return a random value to simulate a condition based on a 50/50 chance
            return Task.FromResult(new Random().Next(0, 2) == 1);
        }
    }
}
