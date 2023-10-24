using Blog.Common.Infrastructure;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;
using Blog.PostsService.Domain.Users;
using Dapper;

namespace Blog.PostsService.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
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

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                SELECT
                u.id as {nameof(UserDto.Id)},
                u.username as {nameof(UserDto.UserName)},
                p.id as {nameof(PostDto.Id)},
                p.user_id as {nameof(PostDto.UserId)}
                p.title as {nameof(PostDto.Title)}
                FROM users u
                JOIN posts p ON u.id = p.user_id
                """;

            var usersDictionary = new Dictionary<Guid, UserDto>();
            var users = await dbConnection.QueryAsync<UserDto, PostDto, UserDto>(sql,
                (user, post) =>
                {
                    if (usersDictionary.TryGetValue(user.Id, out var existingUser))
                    {
                        user = existingUser;
                    }
                    else
                    {
                        usersDictionary.Add(user.Id, user);
                    }

                    if (user.Id == post.UserId)
                        user.Posts.Add(post);

                    return user;
                },
                splitOn: nameof(PostDto.Id));

            return users.Distinct();
        }

        public async Task<UserDto?> GetUserByIdAsync(UserId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            var usersDictionary = new Dictionary<Guid, UserDto>();
            const string sql = $"""
                SELECT
                u.id as {nameof(UserDto.Id)},
                u.username as {nameof(UserDto.UserName)},
                p.id as {nameof(PostDto.Id)},
                p.title as {nameof(PostDto.Title)}
                FROM users u
                JOIN posts p ON u.id = p.user_id
                WHERE u.id = @userId
                """;
            var user = await dbConnection.QueryAsync<UserDto, PostDto, UserDto>(sql,
                (user, post) =>
                {
                    if (usersDictionary.TryGetValue(post.Id, out var existingUser))
                    {
                        user = existingUser;
                    }
                    else
                    {
                        usersDictionary.Add(user.Id, user);
                    }
                    user.Posts.Add(post);

                    return user;
                },
                new { userId = id },
                splitOn: nameof(PostDto.Id));

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
