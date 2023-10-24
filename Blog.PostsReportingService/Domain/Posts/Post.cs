using Blog.PostsReportingService.Domain.PostEvents;

namespace Blog.PostsReportingService.Domain.Posts
{
    public sealed class Post
    {
        public PostId Id { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        public int LikesCount { get; set; }

        public ICollection<PostEvent> Events { get; set; } = new List<PostEvent>();
    }
}
