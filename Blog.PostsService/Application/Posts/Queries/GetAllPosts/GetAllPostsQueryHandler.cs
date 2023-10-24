using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;
using Blog.PostsService.Application.Mappings;
using Blog.PostsService.Domain.Repositories;

namespace Blog.PostsService.Application.Posts.Queries.GetAllPosts
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
            unitOfWork.Commit();
            return _postMapper.MapPostsToGetAllPostsQueryResponse(posts);
        }
    }
}
