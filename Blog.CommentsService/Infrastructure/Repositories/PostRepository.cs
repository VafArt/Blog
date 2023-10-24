using Blog.CommentsService.Domain.Comments;
using Blog.CommentsService.Domain.Posts;
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
                INSERT INTO posts (id, created_on_utc)
                VALUES (@{nameof(Post.Id)}, @{nameof(Post.CreatedOnUtc)});
                """;
            await dbConnection.ExecuteAsync(sql, post);
        }

        public async Task<Post?> GetPostByIdAsync(PostId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                SELECT
                id as {nameof(Comment.Id)},
                created_on_utc as {nameof(Comment.CreatedOnUtc)}
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
