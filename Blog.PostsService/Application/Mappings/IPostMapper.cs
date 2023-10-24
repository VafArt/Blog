using Blog.PostsService.Application.Posts.Commands.CreatePost;
using Blog.PostsService.Application.Posts.Commands.UpdatePost;
using Blog.PostsService.Application.Posts.GetPostById;
using Blog.PostsService.Application.Posts.Queries.GetAllPosts;
using Blog.PostsService.Domain.Posts;

namespace Blog.PostsService.Application.Mappings
{
    public interface IPostMapper
    {
        Post MapCreatePostCommandToPost(CreatePostCommand createPostCommand);
        GetAllPostsQueryResponse MapPostsToGetAllPostsQueryResponse(IEnumerable<Post> posts);
        CreatePostCommandResponse MapPostToCreatePostCommandResponse(Post post);
        GetPostByIdQueryResponse MapPostToGetPostByIdQueryResponse(Post post);
        UpdatePostCommandResponse MapPostToUpdatePostCommandResponse(Post post);
        void MapUpdatePostCommandToPost(UpdatePostCommand updatePostCommand, Post post);
    }
}