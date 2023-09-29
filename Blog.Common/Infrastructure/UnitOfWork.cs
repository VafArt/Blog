using Blog.Common.Domain.Repositories;
using System.Data;
using System.Data.Common;

namespace Blog.Common.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbTransaction _transaction;
        private readonly DbConnection _connection;

        public bool IsDisposed { get; private set; } = false;

        public UnitOfWork(DbConnection dbConnection, IsolationLevel isolationLevel)
        {
            _connection = dbConnection;
            _connection.Open();
            _transaction = _connection.BeginTransaction(isolationLevel);
        }

        public DbConnection Connection
        {
            get { return _connection; }
        }
        public DbTransaction Transaction
        {
            get { return _transaction; }
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
    }
}
