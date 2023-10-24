using Blog.CommentsService.Domain.Comments;
using Microsoft.Extensions.Hosting;

namespace Blog.CommentsService.Domain.Repositories
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetAllCommentsAsync();

        public Task<Comment?> GetCommentByIdAsync(CommentId id);

        public Task CreateCommentAsync(Comment comment);

        public Task UpdateCommentAsync(Comment comment);

        public Task<bool> ContainsAsync(CommentId id);

        public Task DeleteAsync(CommentId id);
    }
}
