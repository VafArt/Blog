using Blog.PostsService.Application.Posts.GetPostById;
using FluentValidation;

namespace Blog.PostsService.Application.Posts.Queries.GetPostById
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator()
        {
            RuleFor(query => query.PostId).NotEmpty().NotNull();
        }
    }
}
