using FluentValidation;

namespace Blog.PostsReportingService.Application.Posts.Commands.LikePost
{
    public class LikePostCommandValidator : AbstractValidator<LikePostCommand>
    {
        public LikePostCommandValidator()
        {
            RuleFor(command => command.PostId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
