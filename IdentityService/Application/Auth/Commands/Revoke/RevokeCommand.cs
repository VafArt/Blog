using Blog.Common.CQRS;

namespace Blog.IdentityService.Application.Auth.Commands.Revoke
{
    public sealed record RevokeCommand(string Username) : ICommand;
}
