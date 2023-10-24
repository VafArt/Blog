

using Blog.CommentsService.Domain.Comments;
using Blog.Common.CQRS;

namespace Blog.CommentsService.Application.Comments.CreateComment
{
    public sealed record CreateCommentCommand(Guid CommentId, Guid UserId, Guid PostId, Guid? ReplyCommentId, string UserName, string Content) : ICommand;
}
