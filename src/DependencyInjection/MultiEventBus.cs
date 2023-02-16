using Microsoft.Extensions.DependencyInjection;
using Ocluse.LiquidSnow.Core.Events.Internal;
using Ocluse.LiquidSnow.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private async Task ExecuteHandler(object? handler, MethodInfo handleMethodInfo, object[] handleMethodArgs)
        {
            if (handler == null)
            {
                return;
            }

            await (Task)handleMethodInfo.Invoke(handler, handleMethodArgs);
        }

        //protected override async Task Publish(Type eventHandlerType, MethodInfo handleMethodInfo, object[] handleMethodArgs)
        //{
        //    IEnumerable<object?> handlers = _serviceProvider.GetServices(eventHandlerType);

        //    List<Task> handleTasks = new List<Task>();

        //    foreach (object? handler in handlers)
        //    {
        //        if (handler == null)
        //        {
        //            continue;
        //        }

        //        Task handleTask = Task.Factory.StartNew(;

        //        handleTasks.Add(handleTask);
        //    }

        //    if (_strategy == PublishStrategy.Sequential)
        //    {
        //        foreach (Task handleTask in handleTasks)
        //        {
        //            handleTask.Start();
        //            await handleTask.ConfigureAwait(false);
        //        }
        //    }
        //    else if (_strategy == PublishStrategy.FireAndForget)
        //    {
        //        foreach (Task handleTask in handleTasks)
        //        {
        //            handleTask.Start();
        //        }
        //    }
        //    else if (_strategy == PublishStrategy.Parallel)
        //    {
        //        await Task.WhenAll(handleTasks).WithAggregatedExceptions();
        //        Task.WhenAll()
        //    }
        //    else if (_strategy == PublishStrategy.FireAndForgetAfterFirst)
        //    {
        //        await Task.WhenAny(handleTasks).WithAggregatedExceptions();
        //    }

        //}

        protected override async Task Publish(Type eventHandlerType, MethodInfo handleMethodInfo, object[] handleMethodArgs)
        {
            IEnumerable<object?> handlers = _serviceProvider
                .GetServices(eventHandlerType);

            List<TaskCompletionSource<bool>> handleTasks = new List<TaskCompletionSource<bool>>();



            if (_strategy == PublishStrategy.Sequential)
            {
                foreach (object? handler in handlers)
                {
                    await ExecuteHandler(handler, handleMethodInfo, handleMethodArgs);
                }
            }
            else if (_strategy == PublishStrategy.FireAndForget)
            {
                foreach (object? handler in handlers)
                {
                    _ = ExecuteHandler(handler, handleMethodInfo, handleMethodArgs);
                }
            }
            else if (_strategy == PublishStrategy.Parallel)
            {
                await Task.WhenAll(handlers.Select(
                    handler => ExecuteHandler(handler, handleMethodInfo, handleMethodArgs)));
            }
            else if (_strategy == PublishStrategy.FireAndForgetAfterFirst)
            {
                await Task.WhenAny(handlers.Select(
                    handler => ExecuteHandler(handler, handleMethodInfo, handleMethodArgs)))
                    .WithAggregatedExceptions();
            }
        }
    }
}
