namespace Blog.PostsService.Domain.Posts
{
    public class Post
    {
        public PostId Id { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public string? PreviewImageUri { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? PublishedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }
    }
}
