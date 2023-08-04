using System.Collections.Generic;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// The data that is passed between steps in an orchestration.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOrchestrationData<out T>
    {
        /// <summary>
        /// The current step of the orchestration. 
        /// </summary>
        /// <remarks>
        /// This value is incremented despite the execution order of steps.
        /// </remarks>
        int CurrentStep { get; }

        /// <summary>
        /// This represents the order of the currently executing step.
        /// </summary>
        /// <remarks>
        /// This value is null during execution of PreOrchestration and FinalOrchestration steps.
        /// </remarks>
        int? CurrentOrder { get; }

        /// <summary>
        /// This represents the total number of unique steps in the orchestration.
        /// </summary>
        int StepCount { get; }

        /// <summary>
        /// The results of each step in the orchestration.
        /// </summary>
        IReadOnlyList<OrchestrationStepResult> Results { get; }

        /// <summary>
        /// The bag of data that is passed between steps in the orchestration.
        /// </summary>
        IOrchestrationBag Bag { get; }

        /// <summary>
        /// The orchestration that is being executed.
        /// </summary>
        T Orchestration { get; }

    }
}
