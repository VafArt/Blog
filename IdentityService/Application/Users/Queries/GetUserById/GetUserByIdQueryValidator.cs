using FluentValidation;

namespace Blog.IdentityService.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}
