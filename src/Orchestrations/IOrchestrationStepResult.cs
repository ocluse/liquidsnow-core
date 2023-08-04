namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// The result of an orchestration step.
    /// </summary>
    public interface IOrchestrationStepResult
    {
        /// <summary>
        /// A value indicating whether the step was successful.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// The data that is returned by the step.
        /// </summary>
        object? Data { get; }

        /// <summary>
        /// The order of the next step to execute. If not set, the next step is executed.
        /// </summary>
        int? JumpToOrder { get; }
    }
}
