using Ocluse.LiquidSnow.Core.Cqrs;
using Ocluse.LiquidSnow.Core.Events;

namespace Ocluse.LiquidSnow.Core.Mediator.Internal
{
    internal class MediatorBuilderOptions : IMediatorBuilderOptions
    {
        private readonly SimpleServiceProvider _serviceProvider = new SimpleServiceProvider();

        public IMediatorBuilderOptions AddHandler<TQuery, TResult>(IQueryHandler<TQuery, TResult> handler) where TQuery : IQuery<TResult>
        {
            _serviceProvider.AddService(typeof(IQueryHandler<TQuery, TResult>), handler);
            return this;
        }

        public IMediatorBuilderOptions AddHandler<TCommand, TResult>(ICommandHandler<TCommand, TResult> handler) where TCommand : ICommand<TResult>
        {
            _serviceProvider.AddService(typeof(ICommandHandler<TCommand, TResult>), handler);
            return this;
        }

        public IMediatorBuilderOptions AddHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            _serviceProvider.AddService(typeof(ICommandHandler<TCommand>), handler);
            return this;
        }

        public IMediatorBuilderOptions AddHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            _serviceProvider.AddService(typeof(IEventHandler<TEvent>), handler);
            return this;
        }

        public IMediatorBuilderOptions AddService(object service)
        {
            _serviceProvider.AddService(service.GetType(), service);
            return this;
        }

        public IMediatorBuilderOptions AddService<TService, TImplementation>(TImplementation service) where TImplementation : class, TService
        {
            _serviceProvider.AddService(typeof(TService), service);
            return this;
        }

        public IMediator Build()
        {
            return MediatorBuilder.Build(_serviceProvider);
        }
    }

}
