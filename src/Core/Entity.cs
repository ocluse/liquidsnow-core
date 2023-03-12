using Ocluse.LiquidSnow.Core.Events;
using System;
using System.Collections.Generic;

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
        
        ///<inheritdoc cref="IEntity.AddAttachedEvent(IEvent)"/>

        public void AddAttachedEvent(IEvent ev)
        {
            _attachedEvents.Add(ev);
        }

        ///<inheritdoc cref="IEntity.RemoveAttachedEvent(IEvent)"/>
        public void RemoveAttachedEvent(IEvent ev)
        {
            _attachedEvents.Remove(ev);
        }

        ///<inheritdoc cref="IEntity.ClearAttachedEvents"/>
        public void ClearAttachedEvents()
        {
            _attachedEvents.Clear();
        }

        ///<inheritdoc cref="IEntity.GetAttachedEvents"/>

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
