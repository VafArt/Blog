using Npgsql;
using System.Data.Common;

namespace Blog.Common.Infrastructure
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString = string.Empty;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection Create()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
