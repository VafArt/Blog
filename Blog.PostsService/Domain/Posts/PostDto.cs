namespace Blog.PostsService.Domain.Posts
{
    public sealed class PostDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}
