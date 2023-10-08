namespace Blog.PostsService.Application.Posts.Queries.GetAllPosts
{
    public class PostVm
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = new();

        public string? PreviewImageUri { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? PublishedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }
    }
}
