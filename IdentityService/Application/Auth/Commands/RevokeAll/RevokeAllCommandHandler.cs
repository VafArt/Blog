using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.IdentityService.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;

namespace Blog.IdentityService.Application.Auth.Commands.RevokeAll
{
    public sealed class RevokeAllCommandHandler : ICommandHandler<RevokeAllCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeAllCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(RevokeAllCommand request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Result.Success();
        }
    }
}
