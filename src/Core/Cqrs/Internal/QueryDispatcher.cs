using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ocluse.LiquidSnow.Core.Cqrs.Internal
{
    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task<TQueryResult> Dispatch<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken)
        {
            Type queryType = query.GetType();

            Type[] typeArgs = { queryType, typeof(TQueryResult) };

            Type queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(typeArgs);

            var methodInfo = queryHandlerType.GetMethod("Handle") ?? throw new InvalidOperationException("Handle method not found on handler");

            //using IServiceScope scope = _serviceProvider.CreateScope();

            var handler = _serviceProvider.GetService(queryHandlerType);

            return (Task<TQueryResult>?)methodInfo.Invoke(handler, new object[] { query, cancellationToken }) ?? throw new InvalidOperationException("Illegal handle method");

        }
    }
}