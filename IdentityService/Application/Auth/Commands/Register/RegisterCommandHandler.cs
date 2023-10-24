using Blog.Common.CQRS;
using Blog.Common.Domain.Roles;
using Blog.Contracts.ApplicationUsers;
using Blog.IdentityService.Domain.ApplicationUsers;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using IdentityResult = Blog.Common.Domain.Results.IdentityResult;

namespace Blog.IdentityService.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPublishEndpoint _publisher;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IPublishEndpoint publisher)
        {
            _userManager = userManager;
            _publisher = publisher;
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
            else
            {
                await _publisher.Publish(new UserCreatedEvent
                {
                    UserId = Guid.Parse(user.Id),
                    UserName = request.Username
                }, CancellationToken.None);
            }

            return IdentityResult.FromAspNetIdentityResult(result);
        }
    }
}
