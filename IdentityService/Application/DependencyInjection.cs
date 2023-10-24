using Blog.IdentityService.Application.Auth;
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

            return services;
        }
    }
}
