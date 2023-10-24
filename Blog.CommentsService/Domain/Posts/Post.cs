using Blog.CommentsService.Domain.Users;

namespace Blog.CommentsService.Domain.Posts
{
    public class Post
    {
        public PostId Id { get; set; } = null!;

        public UserId UserId { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        public DateTime CreatedOnUtc { get; set; }
    }
}
