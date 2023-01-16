using Ocluse.LiquidSnow.Core.Cqrs.Internal;
using Ocluse.LiquidSnow.Core.Events.Internal;
using Ocluse.LiquidSnow.Core.Mediator.Internal;
using System;

namespace Ocluse.LiquidSnow.Core.Mediator
{
    /// <summary>
    /// Utility methods to create a simple mediator used to dispatch commands and queries.
    /// </summary>
    /// <remarks>
    /// This is not a dependency injection container, neither does it provide methods to create one.
    /// The services to be used must be instantiated before hand, this includes the actual handlers
    /// that will be used by the eventual <see cref="IMediator"/>.
    /// A separate package is used to configure dependency injection using the default .NET DI Container.
    /// </remarks>
    public class MediatorBuilder
    {
        /// <summary>
        /// Creates a <see cref="IMediatorBuilderOptions"/> that can be used to configure a <see cref="IMediator"/>
        /// </summary>
        public static IMediatorBuilderOptions Create()
        {
            return new MediatorBuilderOptions();
        }

        /// <summary>
        /// Creates a <see cref="IMediator"/> that will use the provided
        /// <paramref name="serviceProvider"/> to resolve handlers for queries and commands.
        /// </summary>
        /// <param name="serviceProvider">The provider for command and query handlers</param>
        public static IMediator Build(IServiceProvider serviceProvider)
        {
            CommandDispatcher commands = new CommandDispatcher(serviceProvider);
            QueryDispatcher queries = new QueryDispatcher(serviceProvider);
            EventBus events = new EventBus(serviceProvider);
            return new Internal.Mediator(commands, queries, events, serviceProvider);
        }
    }

}
