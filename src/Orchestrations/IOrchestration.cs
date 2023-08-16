using Ocluse.LiquidSnow.Core.Cqrs;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// An action that is executed as an orchestration.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOrchestration<T>
    {

    }

    ///<inheritdoc cref="IOrchestration{T}"/>
    public interface IOrchestration : IOrchestration<Unit>
    {

    }
}
