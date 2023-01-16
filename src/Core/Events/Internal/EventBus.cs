using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Ocluse.LiquidSnow.Core.Events.Internal
{
    internal class EventBus : IEventBus
    {
        protected IServiceProvider _serviceProvider;

        public EventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Publish(IEvent ev, CancellationToken cancellationToken = default)
        {
            Type eventType = ev.GetType();
            
            Type eventHandlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

            Type[] paramTypes = new Type[] { eventType, typeof(CancellationToken) };

            MethodInfo methodInfo = eventHandlerType.GetMethod("Handle", paramTypes) ?? throw new InvalidOperationException("Handle method not found on event handler");

            object[] handleMethodArgs = new object [] { ev, cancellationToken };

            return Publish(eventHandlerType, methodInfo, handleMethodArgs);
        }

        protected virtual Task Publish(Type eventHandlerType, MethodInfo handleMethodInfo, object[] handleMethodArgs)
        {
            object? handler = _serviceProvider.GetService(eventHandlerType);

            if (handler == null)
            {
                throw new InvalidOperationException($"No handler of type {eventHandlerType.Name} has been registered");
            }
            else
            {
                handleMethodInfo.Invoke(handler, handleMethodArgs);
                return Task.CompletedTask;
            }
        }
    }
}
