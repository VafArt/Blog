using Blog.IdentityService.Application.Users.Queries.GetUserById;
using Blog.IdentityService.Application.Users.Queries.GetUserByName;
using Blog.IdentityService.Domain.ApplicationUsers;

namespace Blog.IdentityService.Application.Mappings
{
    public interface IUserMapper 
    {
        public GetUserByIdQueryResponse MapUserToGetUserByIdQueryResponse(ApplicationUser user);

        public GetUserByNameQueryResponse MapUserToGetUserByNameQueryResponse(ApplicationUser user);
    }
}
