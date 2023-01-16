using System;

namespace Ocluse.LiquidSnow.Core.Events
{
    /// <summary>
    /// An action or occurrence in the system that can be reacted to.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// The time when the event occurred.
        /// </summary>
        DateTime Timestamp { get; }
    }
}
