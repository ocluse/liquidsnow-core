using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ocluse.LiquidSnow.Core.Orchestrations.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// Extension methods for configuring orchestration services.
    /// </summary>
    public static class ConfigureServices
    {

        /// <summary>
        /// Adds the orchestrator to the service collection using the default configuration.
        /// </summary>
        public static OrchestratorBuilder AddOrchestrator(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddOrchestrator(options =>
            {
                foreach (var assembly in assemblies)
                {
                    options.Assemblies.Add(assembly);
                }
            });
        }

        /// <summary>
        /// Adds the orchestrator to the service collection using the provided configuration.
        /// </summary>
        public static OrchestratorBuilder AddOrchestrator(this IServiceCollection services, Action<OrchestratorOptions> configureOptions)
        {
            OrchestratorOptions options = new OrchestratorOptions();

            configureOptions(options);

            if (options.Assemblies.Count == 0)
            {
                throw new InvalidOperationException("No assemblies have been provided to source orchestration steps.");
            }

            ServiceDescriptor orchestratorDescriptor = new ServiceDescriptor(typeof(IOrchestrator), typeof(Orchestrator), options.OrchestratorLifetime);

            services.TryAdd(orchestratorDescriptor);

            OrchestratorBuilder builder = new OrchestratorBuilder(options.StepLifetime, services);

            return builder.AddSteps(options.Assemblies);
        }
    }
}
