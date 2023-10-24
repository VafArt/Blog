using Blog.IdentityService.Application.Auth;
using Blog.IdentityService.Application.Mappings;
using FluentValidation;
using System.Reflection;

namespace Blog.IdentityService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSingleton<ITokenProvider, TokenProvider>();

            services.AddSingleton<IUserMapper, UserMapper>();

            return services;
        }
    }
}
