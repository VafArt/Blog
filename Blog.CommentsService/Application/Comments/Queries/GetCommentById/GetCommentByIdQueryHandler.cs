using Blog.CommentsService.Application.Mappings;
using Blog.CommentsService.Domain.Comments;
using Blog.CommentsService.Domain.Errors;
using Blog.CommentsService.Domain.Repositories;
using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;

namespace Blog.CommentsService.Application.Comments.Queries
{
    public class GetCommentByIdQueryHandler : IQueryHandler<GetCommentByIdQuery, Result<GetCommentByIdQueryResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentMapper _commentMapper;

        public GetCommentByIdQueryHandler(IUnitOfWorkFactory unitOfWorkFactory, ICommentRepository commentRepository, ICommentMapper commentMapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
        }

        public async Task<Result<GetCommentByIdQueryResponse>> Handle(GetCommentByIdQuery query, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var comment = await _commentRepository.GetCommentByIdAsync(CommentId.Create(query.CommentId));

            if(comment is null) return Result.Failure(new GetCommentByIdQueryResponse(), DomainErrors.Comment.NotFound(query.CommentId));

            return _commentMapper.MapCommentToGetCommentByIdQueryResponse(comment);
        }
    }
}
