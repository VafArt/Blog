using FluentValidation;

namespace Blog.PostsReportingService.Application.Posts.Queries.GetPostById
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator()
        {
            RuleFor(query => query.PostId).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}
