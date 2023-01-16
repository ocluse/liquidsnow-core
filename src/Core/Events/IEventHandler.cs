﻿using System.Threading;
using System.Threading.Tasks;

namespace Ocluse.LiquidSnow.Core.Events
{
    /// <summary>
    /// Porvides utility method of handling an event upon publishing.
    /// </summary>
    /// <typeparam name="T">The type of event that can be handled by this handler.</typeparam>
    public interface IEventHandler<T> where T : IEvent
    {
        /// <summary>
        /// Handles the event.
        /// </summary>
        Task Handle(T ev, CancellationToken cancellationToken = default);
    }
}
