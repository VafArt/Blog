namespace Blog.PostsReportingService.Application.Posts.Queries.GetAllPosts
{
    public sealed class PostVm
    {
        public Guid PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int LikesCount { get; set; }

        public ICollection<PostEventVm> Events { get; set; } = null!;
    }
}
