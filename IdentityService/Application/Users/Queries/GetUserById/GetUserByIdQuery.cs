using Blog.Common.CQRS;

namespace Blog.IdentityService.Application.Users.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(Guid UserId) : IQuery;
}
