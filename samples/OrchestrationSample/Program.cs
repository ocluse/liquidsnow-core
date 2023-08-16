using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchestrationSample;
using System.Reflection;
using Ocluse.LiquidSnow.Core.Orchestrations;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        Assembly assembly = typeof(SampleHostedService).Assembly;

        services.AddOrchestrator(options =>
        {
            options.Assemblies.Add(assembly);

            //You can try out different publishing strategies here
            options.OrchestratorLifetime = ServiceLifetime.Singleton;
        });
        services.AddHostedService<SampleHostedService>();
        services.AddLogging();
    })
    .Build();

host.Run();