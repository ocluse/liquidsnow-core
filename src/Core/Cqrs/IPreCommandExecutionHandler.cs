﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ocluse.LiquidSnow.Core.Cqrs
{
    /// <summary>
    /// A contract for creating handlers for commands which are executed before the command is executed.
    /// </summary>
    public interface IPreCommandExecutionHandler<TCommand, TCommandResult>
    {
        /// <summary>
        /// Executes this handler.
        /// </summary>
        /// <remarks>
        /// If the handler returns a continue response, the command will be executed.
        /// Otherwise, the command will not be executed and the value returned by this handler will be deemed the result.
        /// </remarks>
        Task<PreExecutionResult> Handle(TCommand command, CancellationToken cancellationToken = default);
    }

    ///<inheritdoc cref="IPreCommandExecutionHandler{TCommand, TCommandResult}"/>
    public interface IPreCommandExecutionHandler<TCommand> : IPreCommandExecutionHandler<TCommand, Unit>
    {
    }
    
}
