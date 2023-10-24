using Blog.CommentsService.Presentation.Services;

namespace Blog.CommentsService.Presentation
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
