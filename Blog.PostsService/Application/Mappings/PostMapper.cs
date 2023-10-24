using Blog.PostsService.Application.Posts.GetPostById;
using Blog.PostsService.Domain.Posts;
using Riok.Mapperly.Abstractions;
using Blog.PostsService.Application.Posts.Queries.GetAllPosts;
using Blog.PostsService.Application.Posts.Commands.CreatePost;
using Blog.PostsService.Application.Posts.Commands.UpdatePost;
using Blog.PostsService.Domain.Users;

namespace Blog.PostsService.Application.Mappings
{
    [Mapper(UseDeepCloning = true)]
    public partial class PostMapper : IPostMapper
    {
        //Update Post Command Response
        [MapProperty(nameof(Post.Id), nameof(UpdatePostCommandResponse.PostId))]
        public partial UpdatePostCommandResponse MapPostToUpdatePostCommandResponse(Post post);

        [MapProperty(nameof(UpdatePostCommand.PostId), nameof(Post.Id))]
        public partial void MapUpdatePostCommandToPost(UpdatePostCommand updatePostCommand, Post post);

        //Get Post By Id Query Response
        public partial GetPostByIdQueryResponse MapPostToGetPostByIdQueryResponse(Post post);

        private string MapTagToString(Tag tag) => tag.Value;

        //Create Post By Create Post Command
        [MapProperty(nameof(CreatePostCommandResponse.PostId), nameof(Post.Id))]
        public partial Post MapCreatePostCommandToPost(CreatePostCommand createPostCommand);

        [MapProperty(nameof(Post.Id), nameof(CreatePostCommandResponse.PostId))]
        public partial CreatePostCommandResponse MapPostToCreatePostCommandResponse(Post post);

        private Tag MapStringToTag(string tag) => new Tag { Value = tag };

        // GetAllPostsQueryResponse
        public GetAllPostsQueryResponse MapPostsToGetAllPostsQueryResponse(IEnumerable<Post> posts)
            => new GetAllPostsQueryResponse { Posts = MapPostsToPostsVm(posts) };
        private partial IEnumerable<PostVm> MapPostsToPostsVm(IEnumerable<Post> post);
        private partial PostVm MapPostToPostVm(Post post);

        // Shared
        private Guid MapPostIdToGuid(PostId postId) => postId.Value;
        private Guid MapUserIdToGuid(UserId userId) => userId.Value;
    }
}
