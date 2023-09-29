using System.Data.Common;

namespace Blog.Common.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        DbConnection Connection { get; }

        DbTransaction Transaction { get; }

        void Commit();

        void Rollback();

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
