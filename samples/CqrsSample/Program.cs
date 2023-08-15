using Microsoft.Extensions.Hosting;
using System.Reflection;
using Ocluse.LiquidSnow.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using CqrsSample;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        Assembly assembly = typeof(SampleHostedService).Assembly;
        services.AddCqrs(assembly);
        services.AddHostedService<SampleHostedService>();
        services.AddLogging();
    })
    .Build();

host.Run();
