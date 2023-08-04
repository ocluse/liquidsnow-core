using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// Options for configuring the orchestration builder.
    /// </summary>
    public class OrchestratorOptions
    {
        /// <summary>
        /// The lifetime of the orchestration steps in the DI container.
        /// </summary>
        public ServiceLifetime StepLifetime { get; set; }
        
        /// <summary>
        /// The lifetime of the orchestrator in the DI container.
        /// </summary>
        public ServiceLifetime OrchestratorLifetime { get; set; }

        /// <summary>
        /// The assemblies to source orchestration steps from.
        /// </summary>
        public ICollection<Assembly> Assemblies { get; set; }

        internal OrchestratorOptions()
        {
            Assemblies = new List<Assembly>();
            StepLifetime = ServiceLifetime.Scoped;
            OrchestratorLifetime = ServiceLifetime.Scoped;
        }
    }
}
