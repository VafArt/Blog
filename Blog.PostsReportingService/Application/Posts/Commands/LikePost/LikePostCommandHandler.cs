using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;
using Blog.PostsReportingService.Domain.Errors;
using Blog.PostsReportingService.Domain.PostEvents;
using Blog.PostsReportingService.Domain.PostEventTypes;
using Blog.PostsReportingService.Domain.Posts;
using Blog.PostsReportingService.Domain.Repositories;

namespace Blog.PostsReportingService.Application.Posts.Commands.LikePost
{
    public class LikePostCommandHandler : ICommandHandler<LikePostCommand, Result>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostEventRepository _postEventRepository;
        private readonly IPostRepository _postRepository;

        public LikePostCommandHandler(IUnitOfWorkFactory unitOfWorkFactory, IPostEventRepository postEventRepository, IPostRepository postRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postEventRepository = postEventRepository;
            _postRepository = postRepository;
        }

        public async Task<Result> Handle(LikePostCommand command, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            if (!await _postRepository.ContainsAsync(PostId.Create(command.PostId))) return Result.Failure(DomainErrors.Post.NotFound(command.PostId));

            await _postEventRepository.CreatePostEventAsync(new PostEvent
            {
                Id = PostEventId.Create(Guid.NewGuid()),
                PostId = PostId.Create(command.PostId),
                CreatedOnUtc = DateTime.UtcNow,
                EventType = PostEventType.Liked
            });

            await unitOfWork.CommitAsync(cancellation);

            return Result.Success();
        }
    }
}
