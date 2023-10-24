using Blog.Common.Domain.Errors;

namespace Blog.IdentityService.Domain.Errors
{
    public class InvalidCredentialsError : Error
    {
        public string Login {  get; set; }

        public string Password { get; set; }

        public InvalidCredentialsError(string login, string password)
            : base("Auth.InvalidCredentials", "Invalid login or password")
        {
            Login = login;
            Password = password;
        }
    }
}
