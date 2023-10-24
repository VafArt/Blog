using Blog.CommentsService.Domain.Comments;

namespace Blog.CommentsService.Domain.Posts
{
    public class Post
    {
        public PostId Id { get; set; } = null!;

        public DateTime CreatedOnUtc { get; set; }
    }
}
