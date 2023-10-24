using Blog.PostsService.Domain.Repositories;
using Blog.PostsService.Infrastructure;
using Blog.PostsService.Infrastructure.Repositories;
using Blog.PostsService.Infrastructure.TypeHandlers;
using Blog.PostsService.Presentation.Services;
using Dapper;

namespace Blog.PostsService.Presentation
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
