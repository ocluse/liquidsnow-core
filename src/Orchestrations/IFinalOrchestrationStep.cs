using System.Threading.Tasks;
using System.Threading;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// The final step of an orchestration. This step is always executed.
    /// </summary>
    public interface IFinalOrchestrationStep<in T, TResult>
        where T : IOrchestration<TResult>
    {
        /// <summary>
        /// Executes the step.
        /// </summary>
        Task<TResult> Execute(IOrchestrationData<T> data, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// The first step of an orchestration. This step is always executed.
    /// </summary>
    public interface IPreOrchestration<in T, TResult>
        where T : IOrchestration<TResult>
    {
        /// <summary>
        /// Execute the step.
        /// </summary>
        Task<OrchestrationStepResult> Execute(IOrchestrationData<T> data, CancellationToken cancellationToken = default);
    }
}
