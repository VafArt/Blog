using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;
using Blog.Contracts.Posts;
using Blog.PostsService.Application.Mappings;
using Blog.PostsService.Domain.Errors;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;
using MassTransit;

namespace Blog.PostsService.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Result<CreatePostCommandResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;
        private readonly IPostMapper _postMapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreatePostCommandHandler(IPostRepository postRepository, IUnitOfWorkFactory unitOfWorkFactory, IPostMapper postMapper, IPublishEndpoint publishEndpoint)
        {
            _postRepository = postRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _postMapper = postMapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<CreatePostCommandResponse>> Handle(CreatePostCommand command, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var post = await _postRepository.GetPostByIdAsync(PostId.Create(command.PostId));
            if (post is not null) return Result.Failure(_postMapper.MapPostToCreatePostCommandResponse(post), DomainErrors.Post.AlreadyExists());

            post = _postMapper.MapCreatePostCommandToPost(command);
            post.Tags = post.Tags
                .Select(tag => new Tag { Value = tag.Value.ToLower() })
                .DistinctBy(tag => tag.Value)
                .ToList();
            post.Id = PostId.Create(command.PostId);
            post.CreatedOnUtc = DateTime.UtcNow;
            await _postRepository.CreatePostAsync(post);
            unitOfWork.Commit();

            await _publishEndpoint.Publish(new PostCreatedEvent(post.Id.Value, post.Title, post.CreatedOnUtc));

            return _postMapper.MapPostToCreatePostCommandResponse(post);
        }
    }
}
