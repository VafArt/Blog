using Blog.Common.CQRS;

namespace Blog.PostsService.Application.Posts.GetPostById
{
    public record GetPostByIdQuery(Guid PostId) : IQuery;
}
