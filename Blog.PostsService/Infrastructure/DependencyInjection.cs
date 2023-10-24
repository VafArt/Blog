using Blog.Common.Infrastructure;
using Blog.PostsService.Domain.Repositories;
using Blog.PostsService.Infrastructure.Repositories;
using Blog.PostsService.Infrastructure.TypeHandlers;
using Dapper;

namespace Blog.PostsService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            SqlMapper.AddTypeHandler(new PostIdHandler());
            SqlMapper.AddTypeHandler(new UserIdHandler());

            services.AddSingleton<IDbInitializer, NpgsqlPostsDbInitializer>();

            return services;
        }
    }
}
