using FluentValidation;
using System.Text.RegularExpressions;

namespace Blog.IdentityService.Application.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(registerCommand => registerCommand.Username).NotEmpty();
            RuleFor(registerCommand => registerCommand.Password).NotEmpty();
            RuleFor(registerCommand => registerCommand.Email)
                .NotEmpty()
                //email address
                .Matches(new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"));
        }
    }
}
