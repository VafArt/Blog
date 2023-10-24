using FluentValidation;

namespace Blog.PostsService.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(command => command.PostId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
