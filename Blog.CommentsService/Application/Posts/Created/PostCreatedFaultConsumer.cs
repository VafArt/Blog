using Blog.Contracts.Posts;
using MassTransit;

namespace Blog.CommentsService.Application.Posts.Created
{
    public class PostCreatedFaultConsumer : IConsumer<Fault<PostCreatedEvent>>
    {
        private readonly ILogger<PostCreatedFaultConsumer> _logger;

        public PostCreatedFaultConsumer(ILogger<PostCreatedFaultConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<PostCreatedEvent>> context)
        {
            _logger.LogError("Exception occured while trying to consume post created event: Exceptions: {@Exceptions}", context.Message.Exceptions);
            return Task.CompletedTask;
        }
    }
}
