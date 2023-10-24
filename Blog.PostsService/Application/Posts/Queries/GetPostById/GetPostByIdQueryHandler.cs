using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;
using Blog.Contracts.Posts;
using Blog.PostsService.Application.Mappings;
using Blog.PostsService.Domain.Errors;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;
using MassTransit;

namespace Blog.PostsService.Application.Posts.GetPostById
{
    internal class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, Result<GetPostByIdQueryResponse>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostMapper _postMapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public GetPostByIdQueryHandler(IUnitOfWorkFactory unitOfWorkFactory, IPostRepository postRepository /*IServiceProvider serviceProvider*/, IPostMapper postMapper, IPublishEndpoint publishEndpoint)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postRepository = postRepository;
            _postMapper = postMapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<GetPostByIdQueryResponse>> Handle(GetPostByIdQuery query, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var post = await _postRepository.GetPostByIdAsync(PostId.Create(query.PostId));

            if (post is null) return Result.Failure(new GetPostByIdQueryResponse { Id = query.PostId }, DomainErrors.Post.NotFound(query.PostId));

            await _publishEndpoint.Publish(new PostViewedEvent(post.Id.Value, DateTime.UtcNow));

            await unitOfWork.CommitAsync();

            return _postMapper.MapPostToGetPostByIdQueryResponse(post);
        }
    }
}
