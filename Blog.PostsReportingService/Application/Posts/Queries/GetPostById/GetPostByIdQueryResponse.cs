namespace Blog.PostsReportingService.Application.Posts.Queries.GetPostById
{
    public sealed class GetPostByIdQueryResponse
    {
        public Guid PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int LikesCount { get; set; }

        public ICollection<PostEventResponse> Events { get; set; } = null!;
    }
}