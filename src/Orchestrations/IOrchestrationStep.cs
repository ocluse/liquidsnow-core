﻿using System.Threading.Tasks;
using System.Threading;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// A step that is executed in an orchestration.
    /// </summary>
    public interface IOrchestrationStep<in T, TResult>
        where T : IOrchestration<TResult>
    {
        /// <summary>
        /// The order in which the step is executed.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Execute the step.
        /// </summary>
        Task<OrchestrationStepResult> Execute(IOrchestrationData<T> data, CancellationToken cancellationToken = default);
    }
}
