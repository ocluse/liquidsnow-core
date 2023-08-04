using System.Threading.Tasks;
using System.Threading;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// An orchestration step that can be skipped based on the result of a condition.
    /// </summary>
    public interface IConditionalOrchestrationStep<in T, TResult> : IOrchestrationStep<T, TResult>
        where T : IOrchestration<TResult>
    {
        /// <summary>
        /// Check if the step can be executed.
        /// </summary>
        Task<bool> CanExecute(IOrchestrationData<T> data, CancellationToken cancellationToken = default);
    }
}
