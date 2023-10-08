using Blog.Common.CQRS;
using Blog.Common.Domain;
using Blog.Common.Domain.Repositories;
using Blog.PostsService.Application.Mappings;
using Blog.PostsService.Domain.Repositories;

namespace Blog.PostsService.Application.Posts.Queries.GetAllPosts
{
    public class GetAllPostsHandler : IQueryHandler<GetAllPostsQuery, Result<GetAllPostsResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;

        public GetAllPostsHandler(IPostRepository postRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _postRepository = postRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Result<GetAllPostsResponse>> Handle(GetAllPostsQuery query, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();
            var posts = await _postRepository.GetAllPostsAsync();
            unitOfWork.Commit();
            return posts.CreateGetAllPostsResponse();
        }
    }
}
