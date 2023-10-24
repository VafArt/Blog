using Blog.IdentityService.Domain.ApplicationUsers;

namespace Blog.IdentityService.Domain.Repositories
{
    public interface IApplicationUserRepository
    {
        public Task<ApplicationUser?> GetUserByIdAsync(Guid userId);

        public Task<ApplicationUser?> GetUserByNameAsync(string userName);
    }
}
