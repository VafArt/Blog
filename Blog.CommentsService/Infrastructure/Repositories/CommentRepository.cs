using Blog.CommentsService.Domain.Comments;
using Blog.CommentsService.Domain.Repositories;
using Blog.Common.Infrastructure;
using Dapper;

namespace Blog.CommentsService.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public CommentRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task<bool> ContainsAsync(CommentId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
               SELECT 
               COUNT(1)
               FROM comments
               WHERE id = @commentId
               """;

            return await dbConnection.ExecuteScalarAsync<bool>(sql, new { commentId = id.Value });
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                INSERT INTO comments (id, user_id, post_id, reply_comment_id, content, likes_count, created_on_utc, modified_on_utc)
                VALUES (@{nameof(Comment.Id)}, @{nameof(Comment.UserId)}, @{nameof(Comment.PostId)}, @{nameof(Comment.ReplyCommentId)}, @{nameof(Comment.Content)}, @{nameof(Comment.LikesCount)}, @{nameof(Comment.CreatedOnUtc)}, @{nameof(Comment.ModifiedOnUtc)});
                """;
            await dbConnection.ExecuteAsync(sql, comment);
        }

        public async Task DeleteAsync(CommentId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
                DELETE FROM comments 
                WHERE id = @commentId;
                """;
            await dbConnection.ExecuteAsync(sql, new { commentId = id.Value });
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                SELECT
                id as {nameof(Comment.Id)},
                user_id as {nameof(Comment.UserId)},
                post_id as {nameof(Comment.PostId)},
                reply_comment_id as {nameof(Comment.ReplyCommentId)},
                content as {nameof(Comment.Content)},
                likes_count as {nameof(Comment.LikesCount)},
                created_on_utc as {nameof(Comment.CreatedOnUtc)},
                modified_on_utc as {nameof(Comment.ModifiedOnUtc)}
                FROM comments
                """;

            var comments = await dbConnection.QueryAsync<Comment>(sql);

            return comments;
        }

        public async Task<Comment?> GetCommentByIdAsync(CommentId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                SELECT
                id as {nameof(Comment.Id)},
                user_id as {nameof(Comment.UserId)},
                post_id as {nameof(Comment.PostId)},
                reply_comment_id as {nameof(Comment.ReplyCommentId)},
                content as {nameof(Comment.Content)},
                likes_count as {nameof(Comment.LikesCount)},
                created_on_utc as {nameof(Comment.CreatedOnUtc)},
                modified_on_utc as {nameof(Comment.ModifiedOnUtc)}
                FROM comments
                WHERE id = @commentId
                """;
            return await dbConnection.QuerySingleOrDefaultAsync<Comment>(sql, new { commentId = id.Value });
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                UPDATE comments
                SET id = @{nameof(Comment.Id)},
                    user_id = @{nameof(Comment.UserId)},
                    post_id = @{nameof(Comment.PostId)},
                    reply_comment_id = @{nameof(Comment.ReplyCommentId)},
                    content = @{nameof(Comment.Content)},
                    likes_count = @{nameof(Comment.LikesCount)},
                    created_on_utc = @{nameof(Comment.CreatedOnUtc)},
                    modified_on_utc = @{nameof(Comment.ModifiedOnUtc)},
                WHERE id = @{nameof(Comment.Id)}
                """;
            await dbConnection.ExecuteAsync(sql, comment);
        }
    }
}
