using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ocluse.LiquidSnow.Core.DependencyInjection
{
    public class CqrsOptions
    {
        public ServiceLifetime HandlerLifetime { get; set; }
        public ServiceLifetime DispatcherLifetime { get; set; }
        public ICollection<Assembly> Assemblies { get; }

        internal CqrsOptions()
        {
            HandlerLifetime = ServiceLifetime.Transient;
            DispatcherLifetime = ServiceLifetime.Scoped;
            Assemblies = new List<Assembly>();
        }
    }
}