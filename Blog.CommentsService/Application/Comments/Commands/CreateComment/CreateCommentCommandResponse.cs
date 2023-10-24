using Blog.CommentsService.Domain.Comments;

namespace Blog.CommentsService.Application.Comments.CreateComment
{
    public sealed class CreateCommentCommandResponse
    {
        public Guid CommentId { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }

        public Guid? ReplyCommentId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int LikesCount { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}