using System;
using Ocluse.LiquidSnow.Core.Cqrs;
using Ocluse.LiquidSnow.Core.Events;

namespace Ocluse.LiquidSnow.Core.Mediator.Internal
{
    internal class Mediator : IMediator
    {
        public ICommandDispatcher Commands { get; }

        public IQueryDispatcher Queries { get; }

        public IServiceProvider Services { get; }

        public IEventBus Events { get; }

        public Mediator(ICommandDispatcher commands, IQueryDispatcher queries, IEventBus events, IServiceProvider services)
        {
            Commands = commands;
            Queries = queries;
            Services = services;
            Events = events;
        }
    }
}
