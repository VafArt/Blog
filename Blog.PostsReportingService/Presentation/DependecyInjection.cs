using Blog.PostsReportingService.Presentation.Services;

namespace Blog.PostsReportingService.Presentation
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddSingleton<IFailureHandler, FailureHandler>();

            return services;
        }
    }
}
