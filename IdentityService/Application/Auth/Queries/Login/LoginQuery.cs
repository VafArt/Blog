using Blog.Common.CQRS;
using Destructurama.Attributed;

namespace Blog.IdentityService.Application.Auth.Queries.Login
{
    public sealed record LoginQuery() : IQuery
    {
        public string Username { get; set; } = string.Empty;

        [NotLogged]
        public string Password { get; set; } = string.Empty;
    }
}
