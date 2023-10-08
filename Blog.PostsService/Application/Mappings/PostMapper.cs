using Blog.PostsService.Application.Posts.GetPostById;
using Blog.PostsService.Domain.Posts;
using Riok.Mapperly.Abstractions;
using Blog.PostsService.Application.Posts.Queries.GetAllPosts;

namespace Blog.PostsService.Application.Mappings
{
    [Mapper]
    public static partial class PostMapper
    {
        public static partial GetPostByIdResponse CreateGetPostByIdResponse(this Post post);

        
        public static GetAllPostsResponse CreateGetAllPostsResponse(this IEnumerable<Post> posts) 
            => new GetAllPostsResponse { Posts = posts.MapPostsToPostsVm() };
        private static partial IEnumerable<PostVm> MapPostsToPostsVm(this IEnumerable<Post> post);
        private static partial PostVm MapPostToPostVm(Post post);

        private static Guid MapPostIdToGuid(PostId postId) => postId.Value;
    }
}
