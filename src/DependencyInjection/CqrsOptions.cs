using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Ocluse.LiquidSnow.Core.DependencyInjection
{
    /// <summary>
    /// Options used to configure how CQRS handlers should be added to a DI Container.
    /// </summary>
    public class CqrsOptions
    {
        /// <summary>
        /// The lifetime that the handlers will have. The default is <see cref="ServiceLifetime.Transient"/>
        /// </summary>
        public ServiceLifetime HandlerLifetime { get; set; }
        
        /// <summary>
        /// The lifetime that the dispatchers will have. The default is <see cref="ServiceLifetime.Scoped"/>
        /// </summary>
        public ServiceLifetime DispatcherLifetime { get; set; }
        
        /// <summary>
        /// A collection of assemblies to scan for CQRS handlers.
        /// </summary>
        public ICollection<Assembly> Assemblies { get; }

        internal CqrsOptions()
        {
            HandlerLifetime = ServiceLifetime.Transient;
            DispatcherLifetime = ServiceLifetime.Scoped;
            Assemblies = new List<Assembly>();
        }
    }
}