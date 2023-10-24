using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;
using Blog.Contracts.Posts;
using Blog.PostsService.Application.Mappings;
using Blog.PostsService.Domain.Errors;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;
using MassTransit;

namespace Blog.PostsService.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHanler : ICommandHandler<UpdatePostCommand, Result<UpdatePostCommandResponse>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;
        private readonly IPostMapper _postMapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdatePostCommandHanler(IUnitOfWorkFactory unitOfWorkFactory, IPostRepository postRepository, IPostMapper postMapper, IPublishEndpoint publishEndpoint)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postRepository = postRepository;
            _postMapper = postMapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<UpdatePostCommandResponse>> Handle(UpdatePostCommand command, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var post = await _postRepository.GetPostByIdAsync(PostId.Create(command.PostId));

            if (post is null) return Result.Failure(new UpdatePostCommandResponse { PostId = command.PostId }, DomainErrors.Post.NotFound(command.PostId));

            _postMapper.MapUpdatePostCommandToPost(command, post);
            post.ModifiedOnUtc = DateTime.UtcNow;
            await _postRepository.UpdatePostAsync(post);
            unitOfWork.Commit();

            await _publishEndpoint.Publish(new PostModifiedEvent
            {
                PostId = post.Id.Value,
                Title = post.Title,
                CreatedOnUtc = post.ModifiedOnUtc ?? DateTime.UtcNow,
            });

            return _postMapper.MapPostToUpdatePostCommandResponse(post);
        }
    }
}
