using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ocluse.LiquidSnow.Core.Cqrs.Internal
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task<TCommandResult> Dispatch<TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellationToken)
        {
            Type commandType = command.GetType();

            Type[] typeArgs = { commandType, typeof(TCommandResult) }; 

            Type commandHandlerType = typeof(ICommandHandler<,>).MakeGenericType(typeArgs);

            Type[] paramTypes = new Type[] { commandType, typeof(CancellationToken) };

            var methodInfo = commandHandlerType.GetMethod("Handle", paramTypes) ?? throw new InvalidOperationException("Handle method not found on handler");

            object? handler = _serviceProvider.GetService(commandHandlerType);

            if (handler == null)
            {
                throw new InvalidOperationException("Failed to get handler for command");
            }

            return (Task<TCommandResult>?)methodInfo.Invoke(handler, new object[] { command, cancellationToken }) ?? throw new InvalidOperationException("Illegal handle method");
        }
    }
}
