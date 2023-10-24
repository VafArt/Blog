using Blog.Common.Infrastructure;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;
using Dapper;
using System.Collections.Generic;

namespace Blog.PostsService.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;
        public PostRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        private async Task CreateTagsAsync(IEnumerable<Tag> tags)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                INSERT INTO tags (value)
                VALUES (@{nameof(Tag.Value)})
                ON CONFLICT (value) DO NOTHING
                """;
            await dbConnection.ExecuteAsync(sql, tags);
        }

        private async Task AddTagsToPostAsync(Guid postId, IEnumerable<Tag> tags)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
                INSERT INTO posts_tags (post_id, tag_value) VALUES (@Id, @Value) 
                ON CONFLICT (post_id, tag_value) DO NOTHING;
                """;
            var insert = tags.Select(tag => new { Id = postId, Value = tag.Value });
            await dbConnection.ExecuteAsync(sql, insert);
        }

        public async Task CreatePostAsync(Post post)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                INSERT INTO posts (id, user_id, title, content, created_on_utc)
                VALUES (@{nameof(Post.Id)}, @{nameof(Post.UserId)}, @{nameof(Post.Title)}, @{nameof(Post.Content)}, @{nameof(Post.CreatedOnUtc)});
                """;
            await dbConnection.ExecuteAsync(sql, post);
            await CreateTagsAsync(post.Tags);
            await AddTagsToPostAsync(post.Id.Value, post!.Tags);
        }   

        public async Task<bool> ContainsAsync(PostId postId)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
               SELECT 
               COUNT(1)
               FROM posts
               WHERE id = @postId
               """;

            return await dbConnection.ExecuteScalarAsync<bool>(sql, new { postId = postId.Value });
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                SELECT
                p.id as {nameof(Post.Id)},
                p.user_id as {nameof(Post.UserId)},
                p.title as {nameof(Post.Title)},
                p.content as {nameof(Post.Content)},
                p.preview_image_uri as {nameof(Post.PreviewImageUri)},
                p.created_on_utc as {nameof(Post.CreatedOnUtc)},
                p.published_on_utc as {nameof(Post.PublishedOnUtc)},
                p.modified_on_utc as {nameof(Post.ModifiedOnUtc)},
                t.tag_value as {nameof(Tag.Value)},
                t.post_id as {nameof(Tag.PostId)}
                FROM posts p
                JOIN posts_tags t ON p.id = t.post_id
                """;

            var postsDictionary = new Dictionary<Guid, Post>();
            var posts = await dbConnection.QueryAsync<Post, Tag, Post>(sql,
                (post, tag) =>
                {
                    if (postsDictionary.TryGetValue(post.Id.Value, out var existingPost))
                    {
                        post = existingPost;
                    }
                    else
                    {
                        postsDictionary.Add(post.Id.Value, post);
                    }

                    if(post.Id == tag.PostId)
                        post.Tags.Add(tag);

                    return post;
                },
                splitOn: nameof(Tag.Value));

            return posts.Distinct();
        }

        public async Task<Post?> GetPostByIdAsync(PostId postId)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            var postsDictionary = new Dictionary<Guid, Post>();
            const string sql = $"""
                SELECT
                p.id as {nameof(Post.Id)},
                p.user_id as {nameof(Post.UserId)},
                p.title as {nameof(Post.Title)},
                p.content as {nameof(Post.Content)},
                p.preview_image_uri as {nameof(Post.PreviewImageUri)},
                p.created_on_utc as {nameof(Post.CreatedOnUtc)},
                p.published_on_utc as {nameof(Post.PublishedOnUtc)},
                p.modified_on_utc as {nameof(Post.ModifiedOnUtc)},
                t.tag_value as {nameof(Tag.Value)}
                FROM posts p
                JOIN posts_tags t ON p.id = t.post_id
                WHERE p.id = @postId
                """;
            var post = await dbConnection.QueryAsync<Post, Tag, Post>(sql, 
                (post, tag) =>
                {
                    if(postsDictionary.TryGetValue(post.Id.Value, out var existingPost))
                    {
                        post = existingPost;
                    }
                    else
                    {
                        postsDictionary.Add(post.Id.Value, post);
                    }
                    post.Tags.Add(tag);

                    return post;
                },
                new { postId = postId.Value },
                splitOn: nameof(Tag.Value));

            return post.FirstOrDefault();
        }

        public async Task UpdatePostAsync(Post post)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = $"""
                UPDATE posts
                SET title = @{nameof(Post.Title)},
                    content = @{nameof(Post.Content)},
                    preview_image_uri = @{nameof(Post.PreviewImageUri)},
                    created_on_utc = @{nameof(Post.CreatedOnUtc)},
                    published_on_utc = @{nameof(Post.PublishedOnUtc)},
                    modified_on_utc = @{nameof(Post.ModifiedOnUtc)}
                WHERE id = @{nameof(Post.Id)}
                """;
            await dbConnection.ExecuteAsync (sql, post);

            await DeletePostTagsAsync(post.Id.Value);
            await CreateTagsAsync(post.Tags);
            await AddTagsToPostAsync(post.Id.Value, post.Tags);
        }

        private async Task DeletePostTagsAsync(Guid postId)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
                DELETE FROM posts_tags 
                WHERE post_id = @postId;
                """;
            await dbConnection.ExecuteAsync(sql, new { postId = postId });
        }

        public async Task DeleteAsync(PostId id)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            const string sql = """
                DELETE FROM posts 
                WHERE id = @postId;
                """;
            await dbConnection.ExecuteAsync(sql, new { postId = id.Value });
            await DeletePostTagsAsync(id.Value);
        }
    }
}
