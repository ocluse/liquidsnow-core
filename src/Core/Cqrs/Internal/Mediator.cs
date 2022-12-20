using System;
using Ocluse.LiquidSnow.Core.Cqrs.Mediator;

namespace Ocluse.LiquidSnow.Core.Cqrs.Internal
{
    internal class Mediator : IMediator
    {
        public ICommandDispatcher Commands { get; }

        public IQueryDispatcher Queries { get; }

        public IServiceProvider Services {get;}

        public Mediator(ICommandDispatcher commands, IQueryDispatcher queries, IServiceProvider services)
        {
            Commands = commands;
            Queries = queries;
            Services = services;
        }
    }
}
