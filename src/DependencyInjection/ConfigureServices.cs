using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ocluse.LiquidSnow.Core.Cqrs;
using Ocluse.LiquidSnow.Core.Cqrs.Internal;
using System.Reflection;

namespace Ocluse.LiquidSnow.Core.DependencyInjection
{

    public static class ConfigureServices
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return services.AddCqrs(assembly);
        }

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

        public static IServiceCollection AddCqrs(this IServiceCollection services, Action<CqrsOptions> configureOptions)
        {
            CqrsOptions options = new();

            configureOptions.Invoke(options);

            if (options.Assemblies.Count < 1)
            {
                throw new InvalidOperationException("You must provide at least one assembly to source CQRS handlers");
            }

            ServiceDescriptor commandDispatcherDescriptor = new(typeof(ICommandDispatcher), typeof(CommandDispatcher), options.DispatcherLifetime);
            
            ServiceDescriptor queryDispatcherDescriptor = new(typeof(IQueryDispatcher), typeof(QueryDispatcher), options.DispatcherLifetime);

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
            List<ServiceDescriptor> descriptors = new();
            assembly.GetTypes()
                .Where(item => item.GetInterfaces()
                .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == type) && !item.IsAbstract && !item.IsInterface)
                .ToList()
                .ForEach(assignedType =>
                {
                    var serviceType = assignedType.GetInterfaces().First(i => i.GetGenericTypeDefinition() == type);

                    ServiceDescriptor descriptor = new(serviceType, assignedType, lifetime);
                    descriptors.Add(descriptor);
                });

            services.TryAdd(descriptors);

            return services;
        }
    }
}