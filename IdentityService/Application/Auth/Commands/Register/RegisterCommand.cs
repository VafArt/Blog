using Blog.Common.CQRS;
using Destructurama.Attributed;

namespace Blog.IdentityService.Application.Auth.Commands.Register
{
    public sealed record RegisterCommand : ICommand
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [NotLogged]
        public string Password { get; set; } = string.Empty;
    }
}
