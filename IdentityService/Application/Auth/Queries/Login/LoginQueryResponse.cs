using Blog.Common.Application.JsonConverters;
using System.Text.Json.Serialization;

namespace Blog.IdentityService.Application.Auth.Queries.Login
{
    public sealed record LoginQueryResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}
