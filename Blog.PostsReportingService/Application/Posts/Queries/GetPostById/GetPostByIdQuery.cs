using Blog.Common.CQRS;

namespace Blog.PostsReportingService.Application.Posts.Queries.GetPostById
{
    public sealed record GetPostByIdQuery(Guid PostId) : IQuery;
}
