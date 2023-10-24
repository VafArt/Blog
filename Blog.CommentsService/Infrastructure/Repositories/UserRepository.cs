using Blog.CommentsService.Domain.Posts;
using Blog.CommentsService.Domain.Repositories;
using Blog.CommentsService.Domain.Users;
using Blog.Common.Infrastructure;
using Dapper;

namespace Blog.CommentsService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public UserRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task<bool> ContainsAsync(UserId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
               SELECT 
               COUNT(1)
               FROM users
               WHERE id = @userId
               """;

            return await dbConnection.ExecuteScalarAsync<bool>(sql, new { userId = id.Value });
        }

        public async Task CreateUserAsync(User user)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                INSERT INTO users (id, username)
                VALUES (@{nameof(User.Id)}, @{nameof(User.UserName)});
                """;
            await dbConnection.ExecuteAsync(sql, user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                SELECT
                u.id as {nameof(User.Id)},
                u.username as {nameof(User.UserName)},
                p.id as {nameof(Post.Id)},
                p.user_id as {nameof(Post.UserId)}
                p.title as {nameof(Post.Title)}
                FROM users u
                JOIN posts p ON u.id = p.user_id
                """;

            var usersDictionary = new Dictionary<Guid, User>();
            var users = await dbConnection.QueryAsync<User, Post, User>(sql,
                (user, post) =>
                {
                    if (usersDictionary.TryGetValue(user.Id.Value, out var existingUser))
                    {
                        user = existingUser;
                    }
                    else
                    {
                        usersDictionary.Add(user.Id.Value, user);
                    }

                    if (user.Id == post.UserId)
                        user.Posts.Add(post);

                    return user;
                },
                splitOn: nameof(Post.Id));

            return users.Distinct();
        }

        public async Task<User?> GetUserByIdAsync(UserId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            var usersDictionary = new Dictionary<Guid, User>();
            const string sql = $"""
                SELECT
                u.id as {nameof(User.Id)},
                u.username as {nameof(User.UserName)},
                p.id as {nameof(Post.Id)},
                p.title as {nameof(Post.Title)}
                FROM users u
                JOIN posts p ON u.id = p.user_id
                WHERE u.id = @userId
                """;
            var user = await dbConnection.QueryAsync<User, Post, User>(sql,
                (user, post) =>
                {
                    if (usersDictionary.TryGetValue(post.Id.Value, out var existingUser))
                    {
                        user = existingUser;
                    }
                    else
                    {
                        usersDictionary.Add(user.Id.Value, user);
                    }
                    user.Posts.Add(post);

                    return user;
                },
                new { userId = id },
                splitOn: nameof(Post.Id));

            return user.FirstOrDefault();
        }

        public async Task UpdateUserAsync(User user)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                UPDATE users
                SET id = @{nameof(User.Id)},
                    username = @{nameof(User.UserName)}
                WHERE id = @{nameof(User.Id)}
                """;
            await dbConnection.ExecuteAsync(sql, user);
        }
    }
}
