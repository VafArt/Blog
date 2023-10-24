using FluentValidation;

namespace Blog.IdentityService.Application.Users.Queries.GetUserByName
{
    public class GetUserByNameQueryValidator : AbstractValidator<GetUserByNameQuery>
    {
        public GetUserByNameQueryValidator()
        {
            RuleFor(query => query.UserName).NotNull().NotEmpty();
        }
    }
}
