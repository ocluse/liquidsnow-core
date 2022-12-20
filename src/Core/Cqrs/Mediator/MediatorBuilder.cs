using Ocluse.LiquidSnow.Core.Cqrs.Internal;
using System;

namespace Ocluse.LiquidSnow.Core.Cqrs.Mediator
{
    public class MediatorBuilder
    {
        public static IMediatorBuilderOptions Create()
        {
            return new MediatorBuilderOptions();
        }

        public static IMediator Build(IServiceProvider serviceProvider)
        {
            return CreateMediator(serviceProvider);
        }

        internal static IMediator CreateMediator(IServiceProvider serviceProvider)
        {
            CommandDispatcher commands = new(serviceProvider);
            QueryDispatcher queries = new(serviceProvider);
            return new Internal.Mediator(commands, queries, serviceProvider);
        }
    }

}
