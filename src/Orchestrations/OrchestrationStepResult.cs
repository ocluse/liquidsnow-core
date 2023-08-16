﻿namespace Ocluse.LiquidSnow.Core.Orchestrations
{

    ///<inheritdoc cref="IOrchestrationStepResult"/>
    public readonly struct OrchestrationStepResult : IOrchestrationStepResult
    {
        ///<inheritdoc cref="IOrchestrationStepResult.IsSuccess"/>
        public bool IsSuccess { get; }

        ///<inheritdoc cref="IOrchestrationStepResult.Data"/>
        public object? Data { get; }

        ///<inheritdoc cref="IOrchestrationStepResult.JumpToOrder"/>
        public int? JumpToOrder { get; }

        /// <summary>
        /// Creates a new instance of <see cref="OrchestrationStepResult"/>.
        /// </summary>
        public OrchestrationStepResult(bool isSuccess, object? data, int? jumpToStep)
        {
            IsSuccess = isSuccess;
            Data = data;
            JumpToOrder = jumpToStep;
        }

        /// <summary>
        /// Returns a result indicating that the step was successfully executed.
        /// </summary>
        public static IOrchestrationStepResult Success(object? data = null, int? jumpToStep = null)
        {
            return new OrchestrationStepResult(true, data, jumpToStep);
        }

        /// <summary>
        /// Returns a result indicating that the step failed.
        /// </summary>
        public static IOrchestrationStepResult Failed(object? data = null, int? jumpToStep = null)
        {
            return new OrchestrationStepResult(false, data, jumpToStep);
        }
    }
}
