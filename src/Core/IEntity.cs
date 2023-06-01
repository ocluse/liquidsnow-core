using Ocluse.LiquidSnow.Core.Events;
using System;
using System.Collections.Generic;

namespace Ocluse.LiquidSnow.Core
{
    /// <summary>
    /// A concept or object about which information is stored
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The unique identifier of this entity
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The timestamp when this entity was first created
        /// </summary>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Attaches an event to the entity
        /// </summary>
        /// <param name="ev">The event to add</param>
        void AddAttachedEvent(IEvent ev);

        /// <summary>
        /// Removes a previously attached event on the en
        /// </summary>
        /// <param name="ev">The event to remove</param>
        void RemoveAttachedEvent(IEvent ev);

        /// <summary>
        /// Removes all events that have been attached on the entity
        /// </summary>
        void ClearAttachedEvents();

        /// <summary>
        /// Returns a list of currently attached events
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<IEvent> GetAttachedEvents();
    }

    ///<inheritdoc cref="IEntity"/>
    public interface IEntity<TModel> : IEntity where TModel : IModel
    {
        /// <summary>
        /// Returns this entity represented as a model
        /// </summary>
        TModel GetModel(object? args = null);
    }
}
