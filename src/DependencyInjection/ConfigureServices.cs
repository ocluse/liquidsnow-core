using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ocluse.LiquidSnow.Core.Cqrs;
using Ocluse.LiquidSnow.Core.Cqrs.Internal;
using Ocluse.LiquidSnow.Core.Events;
using Ocluse.LiquidSnow.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ocluse.LiquidSnow.Core.DependencyInjection
{

    /// <summary>
    /// Extension methods to add CQRS handlers and dispatchers to a DI container.
    /// </summary>
    public static class ConfigureServices
    {
        #region CQRS
        /// <summary>
        /// Adds CQRS handlers from the provided assemblies using default configuration.
        /// </summary>
        public static CqrsBuilder AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddCqrs(options =>
            {
                foreach (var assembly in assemblies)
                {
                    options.Assemblies.Add(assembly);
                }
            });
        }

        /// <summary>
        /// Adds CQRS handlers using the provided options
        /// </summary>
        public static CqrsBuilder AddCqrs(this IServiceCollection services, Action<CqrsOptions> configureOptions)
        {
            CqrsOptions options = new CqrsOptions();

            configureOptions.Invoke(options);

            if (options.Assemblies.Count < 1)
            {
                throw new InvalidOperationException("You must provide at least one assembly to source CQRS handlers");
            }

            ServiceDescriptor commandDispatcherDescriptor = new ServiceDescriptor(typeof(ICommandDispatcher), typeof(CommandDispatcher), options.DispatcherLifetime);
            
            ServiceDescriptor queryDispatcherDescriptor = new ServiceDescriptor(typeof(IQueryDispatcher), typeof(QueryDispatcher), options.DispatcherLifetime);

            services.TryAdd(commandDispatcherDescriptor);
            services.TryAdd(queryDispatcherDescriptor);

            CqrsBuilder builder = new CqrsBuilder(options.HandlerLifetime, services);

            return builder.AddHandlers(options.Assemblies);
        }
        #endregion

        #region EVENT BUS
        /// <summary>
        /// Adds the Event Bus and event handlers from the provided assemblies using the default configuration
        /// </summary>
        public static EventBusBuilder AddEventBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddEventBus(options =>
            {
                options.Assemblies.AddRange(assemblies);
            });
        }

        /// <summary>
        /// Adds the Event Bus and event handlers using the provided options.
        /// </summary>
        public static EventBusBuilder AddEventBus(this IServiceCollection services, Action<EventBusOptions> configureOptions)
        {
            EventBusOptions options = new EventBusOptions();
            configureOptions.Invoke(options);

            if (options.Assemblies.Count < 1)
            {
                throw new InvalidOperationException("You must provide at least one assembly to source event handlers");
            }

            ServiceDescriptor eventBusDescriptor = new ServiceDescriptor(typeof(IEventBus), typeof(MultiEventBus), options.EventBusLifetime);

            services.TryAdd(eventBusDescriptor);

            //Add publishing strategy
            PublishStrategyConfig publishStrategyConfig = new PublishStrategyConfig(options.PublishStrategy);

            services.TryAddSingleton(publishStrategyConfig);

            EventBusBuilder builder = new EventBusBuilder(options.HandlerLifetime, services);

            return builder.AddHandlers(options.Assemblies);
        }


        #endregion

        /// <summary>
        /// Adds all types that implement the provided interface type from the provided assembly to the service collection.
        /// </summary>
        public static IServiceCollection AddImplementers(this IServiceCollection services, Type type, Assembly assembly, ServiceLifetime lifetime, bool ignoreDuplicates = true)
        {
            List<ServiceDescriptor> descriptors = new List<ServiceDescriptor>();
            assembly.GetTypes()
                .Where(item => item.GetInterfaces()
                .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == type) && !item.IsAbstract && !item.IsInterface)
                .ToList()
                .ForEach(assignedType =>
                {
                    var serviceTypes = assignedType.GetInterfaces().Where(i => i.GetGenericTypeDefinition() == type);
                    
                    foreach (var serviceType in serviceTypes)
                    {
                        ServiceDescriptor descriptor = new ServiceDescriptor(serviceType, assignedType, lifetime);
                        descriptors.Add(descriptor);
                    }
                });

            if (ignoreDuplicates)
            {
                services.TryAdd(descriptors);
            }
            else
            {
                services.AddRange(descriptors);
            }
            
            return services;
        }
    }
}