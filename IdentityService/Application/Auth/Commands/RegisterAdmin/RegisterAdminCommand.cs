using Blog.Common.CQRS;
using Destructurama.Attributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IdentityService.Application.Auth.Commands.RegisterAdmin
{
    public sealed record RegisterAdminCommand() : ICommand
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [NotLogged]
        public string Password { get; set; } = string.Empty;
    }
}
