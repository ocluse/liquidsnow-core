using Ocluse.LiquidSnow.Core.Cqrs.Mediator;

namespace Ocluse.LiquidSnow.Core.Cqrs.Internal
{
    internal class MediatorBuilderOptions : IMediatorBuilderOptions
    {
        private readonly SimpleServiceProvider _serviceProvider = new();

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
            return MediatorBuilder.CreateMediator(_serviceProvider);
        }
    }

}
