using Blog.CommentsService.Application.Comments.CreateComment;
using Blog.CommentsService.Application.Comments.Queries;
using Blog.CommentsService.Application.Comments.Queries.GetAllComments;
using Blog.CommentsService.Domain.Comments;

namespace Blog.CommentsService.Application.Mappings
{
    public interface ICommentMapper
    {
        public Comment MapCreateCommentCommandToComment(CreateCommentCommand createCommentCommand);

        public CreateCommentCommandResponse MapCommentToCreateCommentCommandResponse(Comment comment);

        public GetCommentByIdQueryResponse MapCommentToGetCommentByIdQueryResponse(Comment comment);

        public GetAllCommentsQueryResponse MapCommentsToGetAllCommentsQueryResponse(IEnumerable<Comment> comments);
    }
}
