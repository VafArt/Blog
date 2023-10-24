namespace Blog.PostsService.Domain.Posts
{
    public class Tag
    {
        public PostId PostId { get; set; } = null!;
        public string Value { get; set; } = string.Empty;
    }
}
