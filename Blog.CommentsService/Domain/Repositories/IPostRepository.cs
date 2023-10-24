using Blog.CommentsService.Domain.Posts;

namespace Blog.CommentsService.Domain.Repositories
{
    public interface IPostRepository
    {
        public Task CreatePostAsync(Post post);

        public Task<Post?> GetPostByIdAsync(PostId id);

        public Task<bool> ContainsAsync(PostId id);
    }
}
