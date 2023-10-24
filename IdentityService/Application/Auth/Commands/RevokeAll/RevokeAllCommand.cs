
using Blog.Common.CQRS;

namespace Blog.IdentityService.Application.Auth.Commands.RevokeAll
{
    public sealed record RevokeAllCommand() : ICommand;
}
