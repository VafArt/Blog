using Blog.Common.Infrastructure;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;
using Dapper;

namespace Blog.PostsService.Infrastructure.Repositories
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
            const string sql = """
                INSERT INTO posts (id, title, content, created_on_utc)
                VALUES (@Id, @Title, @Content, @CreatedOnUtc)
                """;
            await dbConnection.ExecuteAsync(sql, post);
        }   

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = "SELECT * FROM posts";

            var posts = await dbConnection.QueryAsync<Post>(sql);

            return posts;
        }

        public async Task<Post?> GetPostByIdAsync(PostId postId)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
                SELECT * FROM posts
                WHERE id = @postId
                """;
            var post = await dbConnection.QuerySingleOrDefaultAsync<Post>(sql, new { postId = postId.Value });

            return post;
        }

        public async Task UpdatePostAsync(Post post)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
                UPDATE posts
                SET title = @Title,
                    content = @Content,
                    preview_image_uri = @PreviewImageUri,
                    created_on_utc = @CreatedOnUtc,
                    published_on_utc = @PublishedOnUtc,
                    modified_on_utc = @ModifiedOnUtc
                WHERE id = @Id
                """;
            await dbConnection.ExecuteAsync (sql, post);
        }
    }
}
