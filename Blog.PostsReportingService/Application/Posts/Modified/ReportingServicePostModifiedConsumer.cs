using Blog.Common.Domain.Repositories;
using Blog.Contracts.Posts;
using Blog.PostsReportingService.Domain.PostEvents;
using Blog.PostsReportingService.Domain.PostEventTypes;
using Blog.PostsReportingService.Domain.Posts;
using Blog.PostsReportingService.Domain.Repositories;
using MassTransit;

namespace Blog.PostsReportingService.Application.Posts.Modified
{
    public class ReportingServicePostModifiedConsumer : IConsumer<PostModifiedEvent>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;
        private readonly IPostEventRepository _postEventRepository;

        public ReportingServicePostModifiedConsumer(IUnitOfWorkFactory unitOfWorkFactory, IPostRepository postRepository, IPostEventRepository postEventRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postRepository = postRepository;
            _postEventRepository = postEventRepository;
        }

        public async Task Consume(ConsumeContext<PostModifiedEvent> context)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();


            if(!await _postRepository.ContainsAsync(PostId.Create(context.Message.PostId)))
            {
                //TODO: grpc получение поста из PostsService
                return;
            }

            await _postEventRepository.CreatePostEventAsync(new PostEvent
            {
                Id = PostEventId.Create(Guid.NewGuid()),
                PostId = PostId.Create(Guid.NewGuid()),
                CreatedOnUtc = context.Message.CreatedOnUtc,
                EventType = PostEventType.Modified
            });

            await unitOfWork.CommitAsync();
        }
    }
}
