using Microsoft.AspNetCore.SignalR;

namespace Blog.CommentsService.Domain.Comments
{
    public class Comment
    {
        public CommentId Id { get; set; } = null!;

        public UserId UserId { get; set; } = null!;

        public PostId PostId { get; set; } = null!;

        public CommentId? ReplyCommentId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int LikesCount { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime ModifiedOnUtc { get; set; }
    }
}
