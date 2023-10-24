using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;
using Blog.PostsReportingService.Application.Mappings;
using Blog.PostsReportingService.Domain.Repositories;

namespace Blog.PostsReportingService.Application.Posts.Queries.GetAllPosts
{
    public class GetAllPostsQueryHandler : IQueryHandler<GetAllPostsQuery, Result<GetAllPostsQueryResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;
        private readonly IPostMapper _postMapper;

        public GetAllPostsQueryHandler(IPostRepository postRepository, IUnitOfWorkFactory unitOfWorkFactory, IPostMapper postMapper)
        {
            _postRepository = postRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _postMapper = postMapper;
        }

        public async Task<Result<GetAllPostsQueryResponse>> Handle(GetAllPostsQuery query, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();
            var posts = await _postRepository.GetAllPostsAsync();
            await unitOfWork.CommitAsync();
            return _postMapper.MapPostsToGetAllPostsQueryResponse(posts);
        }
    }
}
