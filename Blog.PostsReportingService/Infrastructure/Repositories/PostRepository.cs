using Blog.Common.Infrastructure;
using Blog.PostsReportingService.Domain.PostEvents;
using Blog.PostsReportingService.Domain.Posts;
using Blog.PostsReportingService.Domain.Repositories;
using Dapper;

namespace Blog.PostsReportingService.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public PostRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
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

        public async Task CreatePostAsync(Post post)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                INSERT INTO posts (id, title)
                VALUES (@{nameof(Post.Id)}, @{nameof(Post.Title)});
                """;
            await dbConnection.ExecuteAsync(sql, post);
            await CreatePostEventsAsync(post.Events);
        }

        private async Task CreatePostEventsAsync(IEnumerable<PostEvent> events)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                INSERT INTO posts_events (id, post_id, created_on_utc, event_type)
                VALUES (@{nameof(PostEvent.Id)}, @{nameof(PostEvent.PostId)}, @{nameof(PostEvent.CreatedOnUtc)}, @{nameof(PostEvent.EventType)})
                ON CONFLICT (id) DO NOTHING
                """;
            await dbConnection.ExecuteAsync(sql, events);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                SELECT
                p.id as {nameof(Post.Id)},
                p.title as {nameof(Post.Title)},
                e.id as {nameof(PostEvent.Id)},
                e.post_id as {nameof(PostEvent.PostId)},
                e.created_on_utc as {nameof(PostEvent.CreatedOnUtc)},
                e.event_type as {nameof(PostEvent.EventType)}
                FROM posts p
                LEFT JOIN posts_events e ON p.id = e.post_id
                """;

            var postsDictionary = new Dictionary<Guid, Post>();
            await dbConnection.QueryAsync<Post, PostEvent, Post>(sql,
                (post, postEvent) =>
                {
                    if (postsDictionary.TryGetValue(post.Id.Value, out var existingPost))
                    {
                        post = existingPost;
                    }
                    else
                    {
                        postsDictionary.Add(post.Id.Value, post);
                    }

                    if (post.Id == postEvent.PostId)
                        post.Events.Add(postEvent);

                    return post;
                },
                splitOn: nameof(PostEvent.Id));

            var postsWithLikesCount = await GetPostsLikesCounts();

            foreach(var post in postsWithLikesCount)
            {
                postsDictionary[post.Id.Value].LikesCount = post.LikesCount;
            }

            return postsDictionary.Values.AsEnumerable();
        }

        private async Task<IEnumerable<Post>> GetPostsLikesCounts()
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            var sql = $"""
                SELECT
                p.id AS {nameof(Post.Id)},
                COUNT(CASE WHEN e.event_type = 5 THEN 1 ELSE NULL END) AS {nameof(Post.LikesCount)}
                FROM posts p LEFT JOIN posts_events e
                ON p.id = e.post_id
                GROUP BY p.id
                """;
            return await dbConnection.QueryAsync<Post>(sql);
        }

        public async Task<Post?> GetPostByIdAsync(PostId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            var postsDictionary = new Dictionary<Guid, Post>();
            const string sql = $"""
                SELECT
                p.id as {nameof(Post.Id)},
                p.title as {nameof(Post.Title)},
                e.id as {nameof(PostEvent.Id)},
                e.post_id as {nameof(PostEvent.PostId)},
                e.created_on_utc as {nameof(PostEvent.CreatedOnUtc)},
                e.event_type as {nameof(PostEvent.EventType)},
                (SELECT
                COUNT(CASE WHEN e.event_type = 1 THEN 1 ELSE NULL END)
                FROM posts p 
                JOIN posts_events e ON p.id = e.post_id
                WHERE p.id = @postId
                GROUP BY p.id) as {nameof(Post.LikesCount)}
                FROM posts p
                LEFT JOIN posts_events e ON p.id = e.post_id
                WHERE p.id = @postId
                """;
            var posts = await dbConnection.QueryAsync<Post, PostEvent, Post>(sql,
                (post, postEvent) =>
                {
                    if (postsDictionary.TryGetValue(post.Id.Value, out var existingPost))
                    {
                        post = existingPost;
                    }
                    else
                    {
                        postsDictionary.Add(post.Id.Value, post);
                    }
                    post.Events.Add(postEvent);

                    return post;
                },
                new { postId = id.Value },
                splitOn: nameof(PostEvent.Id));

            var post = posts.FirstOrDefault();

            await AddLikesCountToPostAsync(post);

            return post;
        }

        private async Task AddLikesCountToPostAsync(Post? post)
        {
            if (post is null) return;

            var dbConnection = _dbConnectionProvider.GetConnection();
            var sql = $"""
                SELECT
                p.id AS {nameof(Post.Id)},
                COUNT(CASE WHEN e.event_type = 5 THEN 1 ELSE NULL END) AS {nameof(Post.LikesCount)}
                FROM posts p LEFT JOIN posts_events e
                ON p.id = e.post_id
                WHERE p.id = @postId
                GROUP BY p.id
                """;
            var postWithLikesCount = await dbConnection.QuerySingleOrDefaultAsync<Post>(sql, new { postId = post.Id.Value });
            post.LikesCount = postWithLikesCount?.LikesCount ?? 0;
        }

        public async Task UpdatePostAsync(Post post)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                UPDATE posts
                SET title = @{nameof(Post.Title)},
                WHERE id = @{nameof(Post.Id)}
                """;
            await dbConnection.ExecuteAsync(sql, post);
            await CreatePostEventsAsync(post.Events);
        }
    }
}
