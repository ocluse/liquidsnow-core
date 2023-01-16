using Ocluse.LiquidSnow.Core.Cqrs;
using Ocluse.LiquidSnow.Core.Events;
using System;

namespace Ocluse.LiquidSnow.Core.Mediator
{
    /// <summary>
    /// Provides a simple interface to dispatch commands and queries
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// The command dispatcher provided by this mediator, used to dispatch commands to their handlers
        /// </summary>
        ICommandDispatcher Commands { get; }

        /// <summary>
        /// The query dispatcher provided by this mediator, used to dispatch queries to their handlers
        /// </summary>
        IQueryDispatcher Queries { get; }

        /// <summary>
        /// The service provider used by this mediator to resolve its handlers
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// The event bus provided by this mediator, used to publish events
        /// </summary>
        IEventBus Events { get; }
    }
}
