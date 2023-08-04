﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Ocluse.LiquidSnow.Core.DependencyInjection;
using System.Reflection;
using System.Linq;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// Builder for adding orchestrations to the service collection.
    /// </summary>
    public class OrchestratorBuilder
    {
        private readonly ServiceLifetime _stepLifetime;

        /// <summary>
        /// Gets the service collection where the orchestration steps are configured.
        /// </summary>
        public IServiceCollection Services { get; }

        internal OrchestratorBuilder(ServiceLifetime handlerLifetime, IServiceCollection services)
        {
            _stepLifetime = handlerLifetime;
            Services = services;
        }

        ///<inheritdoc cref="AddSteps(IEnumerable{Assembly})"/>
        public OrchestratorBuilder AddSteps(params Assembly[] assemblies)
        {
            return AddSteps(assemblies.AsEnumerable());
        }

        /// <summary>
        /// Adds orchestration steps from the provided assemblies.
        /// </summary>
        public OrchestratorBuilder AddSteps(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                Services.AddImplementers(typeof(IOrchestrationStep<,>), assembly, _stepLifetime, false);
                Services.AddImplementers(typeof(IPreOrchestration<,>), assembly, _stepLifetime, false);
                Services.AddImplementers(typeof(IFinalOrchestrationStep<,>), assembly, _stepLifetime, false);
            }
            return this;
        }
    }
}
