using Blog.Common.Domain.Errors;

namespace Blog.IdentityService.Domain.Errors
{
    public class InvalidTokenError : Error
    {
        public string Token { get; set; }

        public InvalidTokenError(string token) 
            : base("Auth.InvalidToken", "Invalid token")
        {
            Token = token;
        }
    }
}
