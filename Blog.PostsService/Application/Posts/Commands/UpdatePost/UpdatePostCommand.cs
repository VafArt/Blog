using Blog.Common.CQRS;

namespace Blog.PostsService.Application.Posts.Commands.UpdatePost
{
    public sealed record UpdatePostCommand(Guid PostId, string Title, string Content, List<string> Tags) : ICommand;
}
