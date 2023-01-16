using Ocluse.LiquidSnow.Core.Cqrs;
using Ocluse.LiquidSnow.Core.Events;

namespace Ocluse.LiquidSnow.Core.Mediator
{
    /// <summary>
    /// Options for configuring a <see cref="IMediator"/>
    /// </summary>
    public interface IMediatorBuilderOptions
    {
        ///<inheritdoc cref="AddService{TService, TImplementation}(TImplementation)"/>
        IMediatorBuilderOptions AddService(object service);

        /// <summary>
        /// Adds the service to the eventual mediator's services stack. 
        /// Use "AddHandler" methods to to properly add command, query, and event handlers instead.
        /// </summary>
        /// <typeparam name="TService">The service to be added</typeparam>
        /// <typeparam name="TImplementation">The implementation of the service</typeparam>
        /// <param name="service">The actual service instance to be added</param>
        IMediatorBuilderOptions AddService<TService, TImplementation>(TImplementation service) where TImplementation : class, TService;

        /// <summary>
        /// Adds a query handler to the eventual mediator's stack that will be used to handle the provided query type.
        /// </summary>
        /// <typeparam name="TQuery">The type of query to handle</typeparam>
        /// <typeparam name="TResult">The type of expected result of the query</typeparam>
        /// <param name="handler">The handler that will handle the provided query</param>
        IMediatorBuilderOptions AddHandler<TQuery, TResult>(IQueryHandler<TQuery, TResult> handler) where TQuery: IQuery<TResult>;

        /// <summary>
        /// Adds a command handler to the eventual mediator stack that will be used to handle the provided command type.
        /// </summary>
        /// <typeparam name="TCommand">The type of command to be handled</typeparam>
        /// <typeparam name="TResult">The type of expected result of the query</typeparam>
        /// <param name="handler">The handler that will handle the provided query</param>
        IMediatorBuilderOptions AddHandler<TCommand, TResult>(ICommandHandler<TCommand, TResult> handler) where TCommand : ICommand<TResult>;

        ///<inheritdoc cref="AddHandler{TCommand, TResult}(ICommandHandler{TCommand, TResult})"/>
        IMediatorBuilderOptions AddHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;

        /// <summary>
        /// Adds an event handler to the eventual mediator's stack that will be invoked on publishing the provided event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of event to be handled</typeparam>
        /// <param name="handler">The handler that will be invoked to handle the event on publish</param>
        IMediatorBuilderOptions AddHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;

        /// <summary>
        /// Returns a mediator ready to dispatch queries and commands with the provided services.
        /// </summary>
        IMediator Build();
    }

}
