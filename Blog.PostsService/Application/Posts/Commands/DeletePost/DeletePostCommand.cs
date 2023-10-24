using Blog.Common.CQRS;
using Blog.PostsService.Domain.Posts;

namespace Blog.PostsService.Application.Posts.Commands.DeletePost
{
    public sealed record DeletePostCommand(Guid PostId) : ICommand;
}
