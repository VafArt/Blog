using Blog.PostsService.Domain.Posts;
using System.Text.Json.Serialization;

namespace Blog.PostsService.Application.Posts.Commands.CreatePost
{
    public sealed class CreatePostCommandResponse
    {
        [JsonPropertyName("postId")]
        public Guid PostId { get; set; }

        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();

        [JsonPropertyName("createdOnUtc")]
        public DateTime CreatedOnUtc { get; set; }
    }
}