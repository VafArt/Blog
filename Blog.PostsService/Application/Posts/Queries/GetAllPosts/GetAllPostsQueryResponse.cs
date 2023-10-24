using System.Text.Json.Serialization;

namespace Blog.PostsService.Application.Posts.Queries.GetAllPosts
{
    public class GetAllPostsQueryResponse
    {
        [JsonPropertyName("posts")]
        public IEnumerable<PostVm>? Posts { get; set; }
    }
}
