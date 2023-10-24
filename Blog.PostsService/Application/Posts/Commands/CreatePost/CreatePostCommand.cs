using Blog.Common.CQRS;

namespace Blog.PostsService.Application.Posts.Commands.CreatePost
{
    public sealed record CreatePostCommand(Guid PostId, Guid UserId, string Title, string Content, List<string> Tags) : ICommand;
}
