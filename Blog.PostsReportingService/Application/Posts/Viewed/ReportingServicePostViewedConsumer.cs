using Blog.Common.Domain.Repositories;
using Blog.Contracts.Posts;
using Blog.PostsReportingService.Domain.PostEvents;
using Blog.PostsReportingService.Domain.PostEventTypes;
using Blog.PostsReportingService.Domain.Posts;
using Blog.PostsReportingService.Domain.Repositories;
using MassTransit;

namespace Blog.PostsReportingService.Application.Posts.Viewed
{
    public class ReportingServicePostViewedConsumer : IConsumer<PostViewedEvent>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostEventRepository _postEventRepository;
        private readonly IPostRepository _postRepository;

        public ReportingServicePostViewedConsumer(IUnitOfWorkFactory unitOfWorkFactory, IPostEventRepository postEventRepository, IPostRepository postRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postEventRepository = postEventRepository;
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<PostViewedEvent> context)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            if(!await _postRepository.ContainsAsync(PostId.Create(context.Message.PostId)))
            {
                //TODO: grpc call to posts service
                return;
            }

            await _postEventRepository.CreatePostEventAsync(new PostEvent
            {
                Id = PostEventId.Create(Guid.NewGuid()),
                PostId = PostId.Create(context.Message.PostId),
                CreatedOnUtc = context.Message.ViewedOnUtc,
                EventType = PostEventType.Viewed
            });

            await unitOfWork.CommitAsync();
        }
    }
}
