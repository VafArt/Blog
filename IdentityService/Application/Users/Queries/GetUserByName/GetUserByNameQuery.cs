using Blog.Common.CQRS;

namespace Blog.IdentityService.Application.Users.Queries.GetUserByName
{
    public sealed record GetUserByNameQuery(string UserName) : IQuery;
}
