using System.Text.Json.Serialization;

namespace Blog.PostsService.Application.Posts.Queries.GetAllPosts
{
    public class PostVm
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();

        [JsonPropertyName("previewImageUri")]
        public string? PreviewImageUri { get; set; }

        [JsonPropertyName("createdOnUtc")]
        public DateTime CreatedOnUtc { get; set; }

        [JsonPropertyName("publishedOnUtc")]
        public DateTime? PublishedOnUtc { get; set; }

        [JsonPropertyName("modifiedOnUtc")]
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
