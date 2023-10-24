using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;
using Blog.PostsReportingService.Application.Mappings;
using Blog.PostsReportingService.Domain.Errors;
using Blog.PostsReportingService.Domain.Posts;
using Blog.PostsReportingService.Domain.Repositories;

namespace Blog.PostsReportingService.Application.Posts.Queries.GetPostById
{
    public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, Result<GetPostByIdQueryResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;
        private readonly IPostMapper _postMapper;

        public GetPostByIdQueryHandler(IUnitOfWorkFactory unitOfWorkFactory, IPostRepository postRepository, IPostMapper postMapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postRepository = postRepository;
            _postMapper = postMapper;
        }

        public async Task<Result<GetPostByIdQueryResponse>> Handle(GetPostByIdQuery query, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var post = await _postRepository.GetPostByIdAsync(PostId.Create(query.PostId));

            if (post is null) return Result.Failure(new GetPostByIdQueryResponse(), DomainErrors.Post.NotFound(query.PostId));

            return _postMapper.MapPostToGetPostByIdQueryResponse(post);
        }
    }
}
