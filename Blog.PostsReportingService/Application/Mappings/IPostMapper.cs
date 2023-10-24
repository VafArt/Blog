using Blog.PostsReportingService.Application.Posts.Queries.GetAllPosts;
using Blog.PostsReportingService.Application.Posts.Queries.GetPostById;
using Blog.PostsReportingService.Domain.Posts;

namespace Blog.PostsReportingService.Application.Mappings
{
    public interface IPostMapper
    {
        GetPostByIdQueryResponse MapPostToGetPostByIdQueryResponse(Post post);

        public GetAllPostsQueryResponse MapPostsToGetAllPostsQueryResponse(IEnumerable<Post> post);
    }
}