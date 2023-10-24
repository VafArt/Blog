using Blog.IdentityService.Domain.ApplicationUsers;
using Blog.IdentityService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.IdentityService.Infrastructure
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly IdentityDbContext _dbContext;

        public ApplicationUserRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => Guid.Parse(user.Id) == userId);
        }

        public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        }
    }
}
