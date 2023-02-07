using Ocluse.LiquidSnow.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocluse.LiquidSnow.Core
{
    ///<inheritdoc cref="IEntity"/>
    public abstract class Entity : IEntity
    {
        private readonly List<IEvent> _attachedEvents = new List<IEvent>();

        ///<inheritdoc cref="IEntity.Id"/>
        public abstract string Id { get; set; }

        ///<inheritdoc cref="IEntity.DateCreated"/>
        public abstract DateTime DateCreated { get; set; }

        /// <summary>
        /// Attaches an event to the entity
        /// </summary>
        /// <param name="ev">The event to add</param>
        public void AddAttachedEvent(IEvent ev)
        {
            _attachedEvents.Add(ev);
        }

        /// <summary>
        /// Removes a previously attached event on the en
        /// </summary>
        /// <param name="ev">The event to remove</param>
        public void RemoveAttachedEvent(IEvent ev)
        {
            _attachedEvents.Remove(ev);
        }

        /// <summary>
        /// Removes all events that have been attached on the entity
        /// </summary>
        public void ClearAttachedEvents()
        {
            _attachedEvents.Clear();
        }

        /// <summary>
        /// Returns a list of currently attached events
        /// </summary>
        /// <returns></returns>

        public IReadOnlyList<IEvent> GetAttachedEvents()
        {
            return _attachedEvents;
        }
    }

    ///<inheritdoc cref="IEntity"/>
    public abstract class Entity<TModel> : Entity, IEntity<TModel> where TModel : IModel
    {
        ///<inheritdoc cref="IEntity{TModel}.GetModel(object?)"/>
        public abstract TModel GetModel(object? args = null);
    }
}
