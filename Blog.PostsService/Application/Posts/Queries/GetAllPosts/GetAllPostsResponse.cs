namespace Blog.PostsService.Application.Posts.Queries.GetAllPosts
{
    public class GetAllPostsResponse
    {
        public IEnumerable<PostVm>? Posts { get; set; }
    }
}
