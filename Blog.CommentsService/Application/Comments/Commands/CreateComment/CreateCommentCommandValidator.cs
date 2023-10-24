using FluentValidation;

namespace Blog.CommentsService.Application.Comments.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(comment => comment.UserId).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(comment => comment.PostId).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(comment => comment.CommentId).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(comment => comment.ReplyCommentId)
                .NotEqual(comment => comment.CommentId).WithMessage("Reply comment ID cannot be equal to comment id");
            RuleFor(comment => comment.UserName).NotEmpty().NotNull();
            RuleFor(comment => comment.Content).NotEmpty().NotNull();
        }
    }
}
