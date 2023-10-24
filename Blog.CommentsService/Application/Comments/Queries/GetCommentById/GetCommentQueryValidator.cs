using FluentValidation;

namespace Blog.CommentsService.Application.Comments.Queries.GetCommentById
{
    public class GetCommentQueryValidator : AbstractValidator<GetCommentByIdQuery>
    {
        public GetCommentQueryValidator()
        {
            RuleFor(query => query.CommentId).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}
