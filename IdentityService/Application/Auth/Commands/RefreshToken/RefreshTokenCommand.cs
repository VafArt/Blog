using Blog.Common.CQRS;

namespace Blog.IdentityService.Application.Auth.Commands.RefreshToken 
{
    public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand;
}
