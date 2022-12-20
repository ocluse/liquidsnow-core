using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ocluse.LiquidSnow.Core.Cqrs;
using Ocluse.LiquidSnow.Core.Cqrs.Internal;
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
        /// <summary>
        /// Adds CQRS handlers from the current assembly using the default configuration.
        /// </summary>
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return services.AddCqrs(assembly);
        }

        /// <summary>
        /// Adds CQRS handlers from the provided assemblies using defautl configuration.
        /// </summary>
        public static IServiceCollection AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
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
        public static IServiceCollection AddCqrs(this IServiceCollection services, Action<CqrsOptions> configureOptions)
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

            foreach (var assembly in options.Assemblies)
            {
                services.AddImplementers(typeof(IQueryHandler<,>), assembly, options.HandlerLifetime);
                services.AddImplementers(typeof(ICommandHandler<,>), assembly, options.HandlerLifetime);
            }

            return services;
        }
       
        private static IServiceCollection AddImplementers(this IServiceCollection services, Type type, Assembly assembly, ServiceLifetime lifetime)
        {
            List<ServiceDescriptor> descriptors = new List<ServiceDescriptor>();
            assembly.GetTypes()
                .Where(item => item.GetInterfaces()
                .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == type) && !item.IsAbstract && !item.IsInterface)
                .ToList()
                .ForEach(assignedType =>
                {
                    var serviceType = assignedType.GetInterfaces().First(i => i.GetGenericTypeDefinition() == type);

                    ServiceDescriptor descriptor = new ServiceDescriptor(serviceType, assignedType, lifetime);
                    descriptors.Add(descriptor);
                });

            services.TryAdd(descriptors);

            return services;
        }
    }
}