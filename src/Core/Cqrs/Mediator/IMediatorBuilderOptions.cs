namespace Ocluse.LiquidSnow.Core.Cqrs.Mediator
{
    /// <summary>
    /// Options for configuring a <see cref="IMediator"/>
    /// </summary>
    public interface IMediatorBuilderOptions
    {
        /// <summary>
        /// Adds the service to the eventual mediator's stack. 
        /// Handlers for various commands and queries should be added here.
        /// </summary>
        /// <param name="service">The service to add</param>
        IMediatorBuilderOptions AddService(object service);
        
        /// <summary>
        /// Adds the service to the eventual medator's services stack.
        /// Handlers for various commands and queris should be added here.
        /// </summary>
        /// <typeparam name="TService">The service to be added</typeparam>
        /// <typeparam name="TImplementation">The implementation of the service</typeparam>
        /// <param name="service">The actual service instance to be added</param>
        IMediatorBuilderOptions AddService<TService, TImplementation>(object service) where TImplementation : class, TService;
        
        /// <summary>
        /// Returns a mediator ready to dispatch queries and commands with the provided services.
        /// </summary>
        /// <returns></returns>
        IMediator Build();
    }

}
