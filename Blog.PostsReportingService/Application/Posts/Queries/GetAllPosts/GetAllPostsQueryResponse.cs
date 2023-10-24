namespace Blog.PostsReportingService.Application.Posts.Queries.GetAllPosts
{
    public class GetAllPostsQueryResponse
    {
        public IEnumerable<PostVm> Posts { get; set; } = null!;
    }
}