using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.IdentityService.Application.Auth;
using Blog.IdentityService.Domain.ApplicationUsers;
using Blog.IdentityService.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.IdentityService.Application.Auth.Queries.Login
{
    public sealed class LoginQueryHandler : IQueryHandler<LoginQuery, Result<LoginQueryResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenProvider _tokenProvider;

        public LoginQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<LoginQueryResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(query.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, query.Password))
                return Result.Failure(new LoginQueryResponse(), DomainErrors.Auth.InvalidCredentials(query.Username, query.Password));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles) claims.Add(new Claim(ClaimTypes.Role, role));

            var token = _tokenProvider.GenerateAccessToken(claims);
            var refreshToken = _tokenProvider.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return new LoginQueryResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }
    }
}
