using Microsoft.Extensions.Hosting;
using Ocluse.LiquidSnow.Core.Orchestrations;

namespace OrchestrationSample
{
    internal class SampleHostedService : IHostedService
    {
        private readonly IOrchestrator _orchestrator;

        public SampleHostedService(IOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            SampleOrchestration sampleOrchestration = new();
            return _orchestrator.Execute(sampleOrchestration, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
