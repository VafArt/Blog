using Blog.IdentityService.Application.Users.Queries.GetUserById;
using Blog.IdentityService.Application.Users.Queries.GetUserByName;
using Blog.IdentityService.Domain.ApplicationUsers;
using Riok.Mapperly.Abstractions;

namespace Blog.IdentityService.Application.Mappings
{
    [Mapper]
    public partial class UserMapper : IUserMapper
    {
        // Get User By Id Query
        [MapProperty(nameof(ApplicationUser.Id), nameof(GetUserByIdQueryResponse.UserId))]
        public partial GetUserByIdQueryResponse MapUserToGetUserByIdQueryResponse(ApplicationUser user);

        // Get User By Name Query
        [MapProperty(nameof(ApplicationUser.Id), nameof(GetUserByNameQueryResponse.UserId))]
        public partial GetUserByNameQueryResponse MapUserToGetUserByNameQueryResponse(ApplicationUser user);

        // Shared
        private Guid MapStringToGuid(string guid) => Guid.TryParse(guid, out var result) ? result : throw new InvalidOperationException();
    }
}
