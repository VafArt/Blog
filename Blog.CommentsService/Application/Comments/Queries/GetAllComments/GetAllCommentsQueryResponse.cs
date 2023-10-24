using Blog.CommentsService.Domain.Comments;

namespace Blog.CommentsService.Application.Comments.Queries.GetAllComments
{
    public class GetAllCommentsQueryResponse
    {
        public IEnumerable<CommentVm> Comments { get; set; } = new List<CommentVm>();
    }
}