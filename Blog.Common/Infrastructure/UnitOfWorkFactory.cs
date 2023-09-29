using Blog.Common.Domain.Repositories;
using System.Data;
using System.Data.Common;

namespace Blog.Common.Infrastructure
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory, IDbConnectionProvider
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IsolationLevel _isolationLevel;
        private UnitOfWork? _unitOfWork;

        private bool IsUnitOfWorkOpen => !(_unitOfWork == null || _unitOfWork.IsDisposed);

        public UnitOfWorkFactory(IDbConnectionFactory connectionFactory, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _isolationLevel = isolationLevel;
            _connectionFactory = connectionFactory;
        }

        public DbConnection GetConnection()
        {
            if (!IsUnitOfWorkOpen)
            {
                throw new InvalidOperationException(
                    "There is no current unit of work from which to get a connection. Call Create first");
            }

            return _unitOfWork!.Connection;
        }

        public IUnitOfWork Create()
        {
            if (IsUnitOfWorkOpen)
            {
                throw new InvalidOperationException(
                    "Cannot begin a transaction before the unit of work from the last one is disposed");
            }

            _unitOfWork = new UnitOfWork(_connectionFactory.Create(), _isolationLevel);
            return _unitOfWork;
        }
    }
}
