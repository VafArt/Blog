using Blog.PostsReportingService.Domain.PostEventTypes;
using Blog.PostsReportingService.Domain.Posts;

namespace Blog.PostsReportingService.Domain.PostEvents
{
    public sealed class PostEvent
    {
        public PostEventId Id { get; set; } = null!;

        public PostId PostId { get; set; } = null!;

        public DateTime CreatedOnUtc { get; set; }

        public PostEventType EventType { get; set; }
    }
}
