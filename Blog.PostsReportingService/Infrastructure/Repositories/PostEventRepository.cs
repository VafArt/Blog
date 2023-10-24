using Blog.Common.Infrastructure;
using Blog.PostsReportingService.Domain.PostEvents;
using Blog.PostsReportingService.Domain.Repositories;
using Dapper;

namespace Blog.PostsReportingService.Infrastructure.Repositories
{
    public class PostEventRepository : IPostEventRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public PostEventRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task CreatePostEventAsync(PostEvent postEvent)
        {
            var dbConnection = _dbConnectionProvider.GetConnection();
            var sql = $"""
                INSERT INTO posts_events (id, post_id, created_on_utc, event_type)
                VALUES (@{nameof(PostEvent.Id)}, @{nameof(PostEvent.PostId)}, @{nameof(PostEvent.CreatedOnUtc)}, @{nameof(PostEvent.EventType)})
                ON CONFLICT DO NOTHING
                """;
            await dbConnection.ExecuteAsync(sql, postEvent);
        }
    }
}
