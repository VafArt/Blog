using Blog.Common.Domain.Results;

namespace Blog.Common.CQRS
{
    public interface IQueryDispatcher
    {
        Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation = default)
            where TQuery : IQuery
            where TQueryResult : Result;
    }
}
