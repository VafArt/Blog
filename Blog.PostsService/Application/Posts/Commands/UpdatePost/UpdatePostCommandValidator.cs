using FluentValidation;

namespace Blog.PostsService.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(command => command.PostId).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(command => command.Title).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(command => command.Content).NotEmpty().NotNull();
            RuleFor(command => command.Tags).NotEmpty().NotNull();
            RuleForEach(command => command.Tags).NotEmpty().NotNull().MaximumLength(30);
        }
    }
}
