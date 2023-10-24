using Blog.Common.Domain.Roles;
using Microsoft.AspNetCore.Identity;

namespace Blog.IdentityService.Infrastructure
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IdentityDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(RoleManager<IdentityRole> roleManager, IdentityDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await _context.SaveChangesAsync();
            await _roleManager.CreateAsync(new IdentityRole(ApplicationRoleDefaults.Admin));
            await _roleManager.CreateAsync(new IdentityRole(ApplicationRoleDefaults.User));
        }
    }
}
