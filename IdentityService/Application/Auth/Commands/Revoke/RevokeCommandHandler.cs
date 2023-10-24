using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.IdentityService.Domain.ApplicationUsers;
using Blog.IdentityService.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace Blog.IdentityService.Application.Auth.Commands.Revoke
{
    public sealed class RevokeCommandHandler : ICommandHandler<RevokeCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(RevokeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return Result.Failure(DomainErrors.ApplicationUser.NotFoundByName(request.Username));

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return Result.Success();
        }
    }
}
