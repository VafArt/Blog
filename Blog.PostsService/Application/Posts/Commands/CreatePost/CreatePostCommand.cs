using Blog.Common.CQRS;

namespace Blog.PostsService.Application.Posts.Commands.CreatePost
{
    public sealed record CreatePostCommand(Guid PostId, string Title, string Content, List<string> Tags) : ICommand;
}
