using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.IdentityService.Application.Auth;
using Blog.IdentityService.Domain.ApplicationUsers;
using Blog.IdentityService.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Blog.IdentityService.Application.Auth.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Result<RefreshTokenCommandResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITokenProvider _tokenProvider;

        public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }

        public async  Task<Result<RefreshTokenCommandResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            var principal = _tokenProvider.GetPrincipalFromToken(request.AccessToken);
            if (principal == null) return Result.Failure(new RefreshTokenCommandResponse(), DomainErrors.Auth.InvalidToken(request.AccessToken));

            var user = await _userManager.FindByNameAsync(principal?.Identity?.Name ?? "");

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return Result.Failure(new RefreshTokenCommandResponse(), DomainErrors.Auth.InvalidToken(request.AccessToken));

            var newAccessToken = _tokenProvider.GenerateAccessToken(principal!.Claims.ToList());
            var newRefreshToken = _tokenProvider.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new RefreshTokenCommandResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
    }
}
