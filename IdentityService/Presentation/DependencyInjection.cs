using Blog.IdentityService.Presentation.Services;

namespace Blog.IdentityService.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddSingleton<IFailureHandler, FailureHandler>();

            return services;
        }
    }
}
