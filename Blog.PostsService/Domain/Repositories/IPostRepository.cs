using Blog.PostsService.Domain.Posts;

namespace Blog.PostsService.Domain.Repositories
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> GetAllPostsAsync();

        public Task<Post?> GetPostByIdAsync(PostId id);

        public Task CreatePostAsync(Post post);

        public Task UpdatePostAsync(Post post);

        public Task<bool> ContainsAsync(PostId id);

        public Task DeleteAsync(PostId id);
    }
}
