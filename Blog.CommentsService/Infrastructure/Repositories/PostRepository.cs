using Blog.CommentsService.Domain.Comments;
using Blog.CommentsService.Domain.Posts;
using Blog.CommentsService.Domain.Repositories;
using Blog.Common.Infrastructure;
using Dapper;

namespace Blog.CommentsService.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public PostRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task CreatePostAsync(Post post)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                INSERT INTO posts (id, user_id, title, created_on_utc)
                VALUES (@{nameof(Post.Id)}, @{nameof(Post.UserId)}, @{nameof(Post.Title)}, @{nameof(Post.CreatedOnUtc)});
                """;
            await dbConnection.ExecuteAsync(sql, post);
        }

        public async Task<Post?> GetPostByIdAsync(PostId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                SELECT
                id as {nameof(Post.Id)},
                user_id as {nameof(Post.UserId)},
                title as {nameof(Post.Title)},
                created_on_utc as {nameof(Post.CreatedOnUtc)}
                FROM posts
                WHERE id = @postId
                """;
            return await dbConnection.QuerySingleOrDefaultAsync<Post>(sql, new { postId = id.Value });
        }

        public async Task<bool> ContainsAsync(PostId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
               SELECT 
               COUNT(1)
               FROM posts
               WHERE id = @postId
               """;

            return await dbConnection.ExecuteScalarAsync<bool>(sql, new { postId = id.Value });
        }
    }
}
