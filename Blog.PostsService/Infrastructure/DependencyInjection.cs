using Blog.Common.CQRS;
using Blog.PostsService.Application.Posts.GetPostById;
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

            SqlMapper.AddTypeHandler(new PostIdHandler());

            return services;
        }
    }
}
