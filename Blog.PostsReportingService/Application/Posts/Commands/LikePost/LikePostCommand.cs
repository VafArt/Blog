
using Blog.Common.CQRS;

namespace Blog.PostsReportingService.Application.Posts.Commands.LikePost
{
    public sealed record LikePostCommand(Guid PostId) : ICommand;
}
