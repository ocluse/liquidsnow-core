using System;

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
        string Id { get; }

        /// <summary>
        /// The timestamp when this entity was first created
        /// </summary>
        DateTime DateCreated { get; }
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
