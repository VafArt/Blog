using Blog.Common.CQRS;
using Blog.Common.Domain;
using Blog.Common.Domain.Repositories;
using Blog.PostsService.Application.Mappings;
using Blog.PostsService.Domain.Errors;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;

namespace Blog.PostsService.Application.Posts.GetPostById
{
    internal class GetPostByIdHandler : IQueryHandler<GetPostByIdQuery, Result<GetPostByIdResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public GetPostByIdHandler(IUnitOfWorkFactory unitOfWorkFactory, IPostRepository postRepository /*IServiceProvider serviceProvider*/)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postRepository = postRepository;
        }

        public async Task<Result<GetPostByIdResponse>> Handle(GetPostByIdQuery query, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var post = await _postRepository.GetPostByIdAsync(PostId.Create(query.PostId));

            if (post is null) return Result.Failure(new GetPostByIdResponse { Id = query.PostId }, DomainErrors.Post.NotFound);

            return post.CreateGetPostByIdResponse();
        }
    }
}
