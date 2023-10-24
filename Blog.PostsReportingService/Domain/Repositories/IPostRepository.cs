using Blog.PostsReportingService.Domain.Posts;

namespace Blog.PostsReportingService.Domain.Repositories
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> GetAllPostsAsync();

        public Task<Post?> GetPostByIdAsync(PostId id);

        public Task<bool> ContainsAsync(PostId id);

        public Task CreatePostAsync(Post post);

        public Task UpdatePostAsync(Post post);
    }
}
