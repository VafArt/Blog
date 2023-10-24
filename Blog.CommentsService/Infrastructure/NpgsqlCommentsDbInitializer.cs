using Blog.Common.Infrastructure;
using Npgsql;
using System.Data.Common;
using System.Data;
using Dapper;

namespace Blog.CommentsService.Infrastructure
{
    public class NpgsqlCommentsDbInitializer : IDbInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<NpgsqlCommentsDbInitializer> _logger;

        public NpgsqlCommentsDbInitializer(IConfiguration configuration, IDbConnectionFactory dbConnectionFactory, ILogger<NpgsqlCommentsDbInitializer> logger)
        {
            _configuration = configuration;
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            InitDb();
            await InitTablesAsync();
        }

        private async Task InitTablesAsync()
        {
            using var connection = _dbConnectionFactory.Create();
            await InitCommentsAsync(connection);
            await InitPostsAsync(connection);
            await InitUsersAsync(connection);
        }

        private async Task InitUsersAsync(DbConnection connection)
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS users (
            	id uuid PRIMARY KEY,
            	username VARCHAR(50) NOT NULL
            );
            """;
            _logger.LogInformation("Initializing users table");
            try
            {
                await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while initializing user table {@Message}, {@Source}, {@StackTrace}", ex.Message, ex.Source, ex.StackTrace);
            }
        }

        private async Task InitPostsAsync(DbConnection connection)
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS posts (
            	id uuid PRIMARY KEY,
                user_id uuid references users(id),
                title VARCHAR(50) NOT NULL,
            	created_on_utc timestamp without time zone NOT NULL
            );
            """;
            _logger.LogInformation("Initializing comments table");
            try
            {
                await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while initializing comments table {@Message}, {@Source}, {@StackTrace}", ex.Message, ex.Source, ex.StackTrace);
            }
        }

        private async Task InitCommentsAsync(DbConnection connection)
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS comments (
            	id uuid PRIMARY KEY,
                user_id uuid NOT NULL,
                post_id uuid NOT NULL,
                reply_comment_id uuid,
            	content text NOT NULL,
                likes_count integer,
            	created_on_utc timestamp without time zone NOT NULL,
            	modified_on_utc timestamp without time zone
            );
            """;
            _logger.LogInformation("Initializing comments table");
            try
            {
                await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while initializing comments table {@Message}, {@Source}, {@StackTrace}", ex.Message, ex.Source, ex.StackTrace);
            }
        }

        private void InitDb()
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(_configuration.GetConnectionString("DefaultConnection"));
            var dbName = connectionStringBuilder.Database;
            connectionStringBuilder.Database = "postgres";

            var checkIfDatabaseExistsSqlCommand = $"""
                SELECT case 
                WHEN oid IS NOT NULL THEN 1 ELSE 0 end 
                FROM pg_database 
                WHERE datname = '{dbName}' limit 1;
                """;
            using var connection = _dbConnectionFactory.Create(connectionStringBuilder.ConnectionString) as NpgsqlConnection;

            connection?.Open();

            // check to see if the database already exists..
            using var command = new NpgsqlCommand(checkIfDatabaseExistsSqlCommand, connection)
            {
                CommandType = CommandType.Text
            };

            _logger.LogInformation("Checking if database exists. Database name: {@DbName}", dbName);

            int? results = null;
            try
            {
                results = (int?)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while checking if database exists {@Message}, {@Source}, {@StackTrace} {@DbName}", ex.Message, ex.Source, ex.StackTrace, dbName);
            }
            // if the database exists, we're done here...
            if (results.HasValue && results.Value == 1)
            {
                _logger.LogInformation("Database exists. Database name: {@DbName}", dbName);
                return;
            }
            _logger.LogInformation("Database does not exists. Creating Database. Database name: {@DbName}", dbName);

            var createDatabaseCommand = $"""
                create database "{dbName}";
                """;

            command.CommandText = createDatabaseCommand;

            // Create the database...
            try
            {
                command?.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while creating database {@Message}, {@Source}, {@StackTrace} {@DbName}", ex.Message, ex.Source, ex.StackTrace, dbName);
            }
        }
    }
}
