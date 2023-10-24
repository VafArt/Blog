using Blog.CommentsService.Application.Comments.CreateComment;
using Blog.CommentsService.Application.Comments.Queries;
using Blog.CommentsService.Application.Comments.Queries.GetAllComments;
using Blog.CommentsService.Domain.Comments;
using Riok.Mapperly.Abstractions;

namespace Blog.CommentsService.Application.Mappings
{
    [Mapper]
    public partial class CommentMapper : ICommentMapper
    {
        // Create Comment Command
        [MapProperty(nameof(CreateCommentCommand.CommentId), nameof(Comment.Id))]
        public partial Comment MapCreateCommentCommandToComment(CreateCommentCommand createCommentCommand);

        [MapProperty(nameof(Comment.Id), nameof(CreateCommentCommandResponse.CommentId))]
        public partial CreateCommentCommandResponse MapCommentToCreateCommentCommandResponse(Comment comment);

        // Shared
        //private CommentId MapGuidToCommentId(Guid commentId) => CommentId.Create(commentId);
        //private UserId MapGuidToUserId(Guid userId) => UserId.Create(userId);
        //private PostId MapGuidToPostId(Guid postId) => PostId.Create(postId);
        private Guid MapCommentIdToGuid(CommentId commentId) => commentId.Value;
        private Guid MapUserIdToGuid(UserId userId) => userId.Value;
        private Guid MapPostIdToGuid(PostId postId) => postId.Value;

        // Get by id
        [MapProperty(nameof(Comment.Id), nameof(GetCommentByIdQueryResponse.CommentId))]
        public partial GetCommentByIdQueryResponse MapCommentToGetCommentByIdQueryResponse(Comment comment);

        //Get all comments
        public partial CommentVm MapCommentToCommentVm(Comment comment);
        public GetAllCommentsQueryResponse MapCommentsToGetAllCommentsQueryResponse(IEnumerable<Comment> comments) 
            => new GetAllCommentsQueryResponse { Comments = MapCommentsToCommentsVm(comments) };
        private partial IEnumerable<CommentVm> MapCommentsToCommentsVm(IEnumerable<Comment> comments);
    }
}
