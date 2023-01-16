using Microsoft.Extensions.DependencyInjection;
using Ocluse.LiquidSnow.Core.Events.Internal;
using Ocluse.LiquidSnow.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Ocluse.LiquidSnow.Core.DependencyInjection
{
    internal class MultiEventBus : EventBus
    {
        private readonly PublishStrategy _strategy;
        public MultiEventBus(PublishStrategyConfig publishStrategyConfig, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _strategy = publishStrategyConfig.Strategy;
        }

        protected override async Task Publish(Type eventHandlerType, MethodInfo handleMethodInfo, object[] handleMethodArgs)
        {
            IEnumerable<object?> handlers = _serviceProvider.GetServices(eventHandlerType);

            List<Task> handleTasks = new List<Task>();

            foreach (object? handler in handlers)
            {
                if (handler == null)
                {
                    continue;
                }

                Task handleTask = (Task)handleMethodInfo.Invoke(handler, handleMethodArgs);
                handleTasks.Add(handleTask);
            }

            if (_strategy == PublishStrategy.Sequential)
            {
                foreach (Task handleTask in handleTasks)
                {
                    await handleTask.ConfigureAwait(false);
                }
            }
            else if (_strategy == PublishStrategy.FireAndForget)
            {
                foreach (Task handleTask in handleTasks)
                {
                    handleTask.Start();
                }
            }
            else if (_strategy == PublishStrategy.Parallel)
            {
                await Task.WhenAll(handleTasks).WithAggregatedExceptions();
            }
            else if (_strategy == PublishStrategy.FireAndForgetAfterFirst)
            {
                await Task.WhenAny(handleTasks).WithAggregatedExceptions();
            }

        }
    }
}
