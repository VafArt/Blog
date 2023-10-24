using Blog.PostsReportingService.Domain.PostEventTypes;
using System.Text.Json.Serialization;

namespace Blog.PostsReportingService.Application.Posts.Queries.GetPostById
{
    public class PostEventResponse
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PostEventType EventType { get; set; }
    }
}