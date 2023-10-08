using Blog.Common.Domain;

namespace Blog.Common.CQRS
{
    public interface IQueryDispatcher
    {
        Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
            where TQuery : IQuery
            where TQueryResult : Result;
    }
}
