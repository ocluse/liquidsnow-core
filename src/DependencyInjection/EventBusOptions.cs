using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Ocluse.LiquidSnow.Core.DependencyInjection
{
    /// <summary>
    /// Options used to configure how the Event Bus and event handlers should be added to a DI Container
    /// </summary>
    public class EventBusOptions
    {
        internal EventBusOptions() 
        {
            HandlerLifetime= ServiceLifetime.Transient;
            EventBusLifetime = ServiceLifetime.Scoped;
            PublishStrategy = PublishStrategy.Sequential;
            Assemblies = new List<Assembly>();
        }

        /// <summary>
        /// The lifetime that will be assigned to all event handlers
        /// </summary>
        public ServiceLifetime HandlerLifetime { get; set; }
        
        /// <summary>
        /// The lifetime that will be assigned to the event bus
        /// </summary>
        public ServiceLifetime EventBusLifetime { get; set; }
        
        /// <summary>
        /// The strategy that the event bus will use to invoke event handlers
        /// </summary>
        public PublishStrategy PublishStrategy { get; set; }
        
        /// <summary>
        /// The assemblies from which event handlers will be sourced
        /// </summary>
        public ICollection<Assembly> Assemblies { get; }
    }
}