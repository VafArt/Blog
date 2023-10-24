using Blog.Common.CQRS;

namespace Blog.CommentsService.Application.Comments.Commands.DeleteComment
{
    public sealed record DeleteCommentCommand(Guid CommentId) : ICommand;
}
