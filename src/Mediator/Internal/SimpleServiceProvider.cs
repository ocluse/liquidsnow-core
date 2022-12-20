using System;
using System.Collections.Generic;

namespace Ocluse.LiquidSnow.Core.Mediator.Internal
{
    internal class SimpleServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public object? GetService(Type serviceType)
        {
            if (_services.TryGetValue(serviceType, out object service))
            {
                return service;
            }
            else
            {
                return null;
            }
        }

        public void AddService(Type serviceType, object service)
        {
            if (_services.ContainsKey(serviceType))
            {
                throw new InvalidOperationException("Service of this type has already been added");
            }

            _services.Add(serviceType, service);
        }
    }
}
