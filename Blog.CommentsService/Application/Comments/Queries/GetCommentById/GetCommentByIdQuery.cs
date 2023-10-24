using Blog.Common.CQRS;

namespace Blog.CommentsService.Application.Comments.Queries
{
    public sealed record GetCommentByIdQuery(Guid CommentId) : IQuery;
}
