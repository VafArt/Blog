using Npgsql;
using System.Data.Common;

namespace Blog.Common.Infrastructure
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString = string.Empty;
        private NpgsqlDataSource _dataSource;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
            _dataSource = NpgsqlDataSource.Create(_connectionString);
        }

        public DbConnection Create()
        {
            return _dataSource.CreateConnection();
        }

        public DbConnection Create(string connectionString)
        {
            var dataSource = NpgsqlDataSource.Create(connectionString);
            return dataSource.CreateConnection();
        }
    }
}
