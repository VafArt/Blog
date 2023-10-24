using Blog.CommentsService.Application.Mappings;
using Blog.CommentsService.Domain.Repositories;
using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;

namespace Blog.CommentsService.Application.Comments.Queries.GetAllComments
{
    public class GetAllCommentsQueryHandler : IQueryHandler<GetAllCommentsQuery, Result<GetAllCommentsQueryResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentMapper _commentMapper;

        public GetAllCommentsQueryHandler(ICommentMapper commentMapper, ICommentRepository commentRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _commentMapper = commentMapper;
            _commentRepository = commentRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Result<GetAllCommentsQueryResponse>> Handle(GetAllCommentsQuery query, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var comments = await _commentRepository.GetAllCommentsAsync();

            await unitOfWork.CommitAsync(cancellation);

            return _commentMapper.MapCommentsToGetAllCommentsQueryResponse(comments);
        }
    }
}
