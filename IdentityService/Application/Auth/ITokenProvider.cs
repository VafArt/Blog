using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.IdentityService.Application.Auth
{
    public interface ITokenProvider
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
