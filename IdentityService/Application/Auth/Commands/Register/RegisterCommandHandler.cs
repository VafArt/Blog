using Blog.Common.CQRS;
using Blog.Common.Domain.Roles;
using Blog.IdentityService.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using IdentityResult = Blog.Common.Domain.Results.IdentityResult;

namespace Blog.IdentityService.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) return IdentityResult.FromAspNetIdentityResult(result);

            result = await _userManager.AddToRoleAsync(user, ApplicationRoleDefaults.User);

            if (!result.Succeeded)
                await _userManager.DeleteAsync(user);

            return IdentityResult.FromAspNetIdentityResult(result);
        }
    }
}
