using Blog.Common.Domain;

namespace Blog.Common.CQRS
{
    public interface IQueryDispatcher
    {
        Task<Result<TQueryResult>> Dispatch<TQueryResult>(IQuery query, CancellationToken cancellation);
    }
}
