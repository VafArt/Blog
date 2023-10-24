using Blog.PostsReportingService.Application.Posts.Queries.GetAllPosts;
using Blog.PostsReportingService.Application.Posts.Queries.GetPostById;
using Blog.PostsReportingService.Domain.PostEvents;
using Blog.PostsReportingService.Domain.Posts;
using Riok.Mapperly.Abstractions;

namespace Blog.PostsReportingService.Application.Mappings
{
    [Mapper]
    public partial class PostMapper : IPostMapper
    {
        // Get Post by id
        [MapProperty(nameof(Post.Id), nameof(GetPostByIdQueryResponse.PostId))]
        public partial GetPostByIdQueryResponse MapPostToGetPostByIdQueryResponse(Post post);

        private partial PostEventResponse MapPostEventToPostEventResponse(PostEvent postEvent);

        //Get All Posts
        public GetAllPostsQueryResponse MapPostsToGetAllPostsQueryResponse(IEnumerable<Post> post)
            => new GetAllPostsQueryResponse { Posts = MapPostsToPostVms(post) };

        private partial IEnumerable<PostVm> MapPostsToPostVms(IEnumerable<Post> post);

        [MapProperty(nameof(Post.Id), nameof(PostVm.PostId))]
        private partial PostVm MapPostToPostVm(Post post);

        private partial PostEventVm MapPostEventToPostEventVm(PostEvent postEvent);


        //shared
        private Guid MapPostIdToGuid(PostId postId) => postId.Value;
        private Guid MapPostEventIdToGuid(PostEventId postEventId) => postEventId.Value;
    }
}
