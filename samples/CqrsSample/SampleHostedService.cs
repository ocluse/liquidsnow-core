using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocluse.LiquidSnow.Core.Cqrs;

namespace CqrsSample
{
    internal class SampleHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SampleHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            RunAsync();
            return Task.CompletedTask;
        }

        private async void RunAsync()
        {
            while (true)
            {
                await Task.Delay(1000);
                await DispatchCommands();
            }
        }

        private async Task DispatchCommands()
        {
            using var scope = _serviceProvider.CreateScope();
            var dispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();
            await dispatcher.Dispatch(new SampleCommand(Guid.NewGuid().ToString()), default);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
