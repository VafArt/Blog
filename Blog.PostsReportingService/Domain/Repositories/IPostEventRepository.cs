using Blog.PostsReportingService.Domain.PostEvents;

namespace Blog.PostsReportingService.Domain.Repositories
{
    public interface IPostEventRepository
    {
        public Task CreatePostEventAsync(PostEvent postEvent);
    }
}
