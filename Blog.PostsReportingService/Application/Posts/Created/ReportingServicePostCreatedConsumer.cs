using Blog.Common.Domain.Repositories;
using Blog.Contracts.Posts;
using Blog.PostsReportingService.Domain.PostEvents;
using Blog.PostsReportingService.Domain.PostEventTypes;
using Blog.PostsReportingService.Domain.Posts;
using Blog.PostsReportingService.Domain.Repositories;
using MassTransit;

namespace Blog.PostsReportingService.Application.Posts.Created
{
    public class ReportingServicePostCreatedConsumer : IConsumer<PostCreatedEvent>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;

        public ReportingServicePostCreatedConsumer(IUnitOfWorkFactory unitOfWorkFactory, IPostRepository postRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<PostCreatedEvent> context)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var post = new Post
            {
                Id = PostId.Create(context.Message.PostId),
                Title = context.Message.Title,
                Events = new List<PostEvent>
                {
                    new PostEvent
                    {
                        Id = PostEventId.Create(Guid.NewGuid()),
                        PostId = PostId.Create(context.Message.PostId),
                        CreatedOnUtc = context.Message.CreatedOnUtc,
                        EventType = PostEventType.Created
                    }
                }
            };

            await _postRepository.CreatePostAsync(post);

            await unitOfWork.CommitAsync(context.CancellationToken);
        }
    }
}
