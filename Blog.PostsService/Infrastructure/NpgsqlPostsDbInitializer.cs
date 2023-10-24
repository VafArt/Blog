using Blog.Common.Infrastructure;
using Dapper;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Xml.Linq;

namespace Blog.PostsService.Infrastructure
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
            await InitUsersAsync(connection);
            await InitPostsAsync(connection);
            await InitTagsAsync(connection);
            await InitPosts_TagsAsync(connection);
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
            	content text NOT NULL,
            	preview_image_uri text,
            	created_on_utc timestamp without time zone NOT NULL,
            	published_on_utc timestamp without time zone,
            	modified_on_utc timestamp without time zone
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

        private async Task InitTagsAsync(DbConnection connection)
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS tags (
            	value VARCHAR(30) PRIMARY KEY
            );
            """;
            _logger.LogInformation("Initializing tags table");
            try
            {
                await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while initializing tags table {@Message}, {@Source}, {@StackTrace}", ex.Message, ex.Source, ex.StackTrace);
            }
        }

        private async Task InitPosts_TagsAsync(DbConnection connection)
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS posts_tags (
            	post_id uuid,
            	tag_value VARCHAR(30),
                PRIMARY KEY(post_id, tag_value)
            );
            """;

            _logger.LogInformation("Initializing posts_tags table");
            try
            {
                await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Application exception occured while initializing posts_tags table {@Message}, {@Source}, {@StackTrace}", ex.Message, ex.Source, ex.StackTrace);
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
