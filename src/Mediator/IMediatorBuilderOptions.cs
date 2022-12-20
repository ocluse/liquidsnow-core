using Ocluse.LiquidSnow.Core.Cqrs;

namespace Ocluse.LiquidSnow.Core.Mediator
{
    /// <summary>
    /// Options for configuring a <see cref="IMediator"/>
    /// </summary>
    public interface IMediatorBuilderOptions
    {
        /// <summary>
        /// Adds the service to the eventual mediator's stack. 
        /// Use <see cref="AddHandler{TCommand, TResult}(ICommandHandler{TCommand, TResult})"/> 
        /// and <see cref="AddHandler{TQuery, TResult}(IQueryHandler{TQuery, TResult})"/> 
        /// to properly add command and query handlers instead.
        /// </summary>
        /// <param name="service">The service to add</param>
        IMediatorBuilderOptions AddService(object service);

        /// <summary>
        /// Adds the service to the eventual medator's services stack.
        /// Use <see cref="AddHandler{TCommand, TResult}(ICommandHandler{TCommand, TResult})"/> 
        /// and <see cref="AddHandler{TQuery, TResult}(IQueryHandler{TQuery, TResult})"/> 
        /// to properly add command and query handlers instead.
        /// </summary>
        /// <typeparam name="TService">The service to be added</typeparam>
        /// <typeparam name="TImplementation">The implementation of the service</typeparam>
        /// <param name="service">The actual service instance to be added</param>
        IMediatorBuilderOptions AddService<TService, TImplementation>(TImplementation service) where TImplementation : class, TService;

        /// <summary>
        /// Adds a query handler to the eventual mediator's stack that will be used to handle the provided query.
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="handler"></param>
        IMediatorBuilderOptions AddHandler<TQuery, TResult>(IQueryHandler<TQuery, TResult> handler) where TQuery: IQuery<TResult>;

        /// <summary>
        /// Adds a command handler to the eventual mediator stack that will be used to handle the provided command.
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="handler"></param>
        IMediatorBuilderOptions AddHandler<TCommand, TResult>(ICommandHandler<TCommand, TResult> handler) where TCommand : ICommand<TResult>;

        /// <summary>
        /// Returns a mediator ready to dispatch queries and commands with the provided services.
        /// </summary>
        IMediator Build();
    }

}
