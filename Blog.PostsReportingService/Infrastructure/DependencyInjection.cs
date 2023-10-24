using Blog.Common.Infrastructure;
using Blog.PostsReportingService.Domain.Repositories;
using Blog.PostsReportingService.Infrastructure.Repositories;
using Blog.PostsReportingService.Infrastructure.TypeHandlers;
using Dapper;

namespace Blog.PostsReportingService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostEventRepository, PostEventRepository>();

            SqlMapper.AddTypeHandler(new PostIdHandler());
            SqlMapper.AddTypeHandler(new PostEventIdHandler());

            services.AddSingleton<IDbInitializer, NpgsqlPostsDbInitializer>();

            return services;
        }
    }
}
