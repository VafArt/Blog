using FluentValidation;

namespace Blog.IdentityService.Application.Auth.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(loginQuery => loginQuery.Username).NotEmpty();
            RuleFor(loginQuery => loginQuery.Password).NotEmpty();
        }
    }
}
