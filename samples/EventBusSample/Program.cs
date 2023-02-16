// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Ocluse.LiquidSnow.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using EventBusSample;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        Assembly assembly = typeof(SampleService).Assembly;
        services.AddEventBus(options =>
        {
            options.Assemblies.Add(assembly);

            //You can try out different publishing strategies here
            options.PublishStrategy = PublishStrategy.FireAndForget;
        });
        services.AddHostedService<SampleService>();
        services.AddLogging();
    })
    .Build();

host.Run();