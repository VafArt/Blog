using Blog.Common.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Common.CQRS
{
    class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task<Result<TQueryResult>> Dispatch<TQueryResult>(IQuery query, CancellationToken cancellation)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<IQuery, TQueryResult>>();
            return handler.Handle(query, cancellation);
        }
    }
}
