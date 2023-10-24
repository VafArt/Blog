using Blog.Common.Infrastructure;
using Dapper;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Xml.Linq;

namespace Blog.PostsReportingService.Infrastructure
{
    public class NpgsqlPostsDbInitializer : IDbInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<NpgsqlPostsDbInitializer> _logger;

        public NpgsqlPostsDbInitializer(IConfiguration configuration, IDbConnectionFactory dbConnectionFactory, ILogger<NpgsqlPostsDbInitializer> logger)
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
            await InitPostsAsync(connection);
            await InitPostsEventsAsync(connection);
        }

        private async Task InitPostsAsync(DbConnection connection)
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS posts (
            	id uuid PRIMARY KEY,
            	title VARCHAR(50) NOT NULL
            );
            """;
            _logger.LogInformation("Initializing posts table");
            try
            {
                await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while initializing posts table {@Message}, {@Source}, {@StackTrace}", ex.Message, ex.Source, ex.StackTrace);
            }
        }

        private async Task InitPostsEventsAsync(DbConnection connection)
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS posts_events (
            	id uuid PRIMARY KEY,
                post_id uuid references posts(id),
                created_on_utc timestamp without time zone NOT NULL,
                event_type integer NOT NULL
            );
            """;
            _logger.LogInformation("Initializing posts_events table");
            try
            {
                await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while initializing posts_events table {@Message}, {@Source}, {@StackTrace}", ex.Message, ex.Source, ex.StackTrace);
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
