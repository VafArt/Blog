using Blog.IdentityService.Domain.ApplicationUsers;
using Blog.IdentityService.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.IdentityService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            return services;
        }
    }
}
