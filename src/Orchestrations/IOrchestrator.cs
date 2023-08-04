﻿using System.Threading.Tasks;
using System.Threading;

namespace Ocluse.LiquidSnow.Core.Orchestrations
{
    /// <summary>
    /// An orchestrator that executes an orchestration.
    /// </summary>
    public interface IOrchestrator
    {
        /// <summary>
        /// Execute an orchestration.
        /// </summary>
        Task<T> Execute<T>(IOrchestration<T> orchestration, CancellationToken cancellationToken = default);
    }
}
