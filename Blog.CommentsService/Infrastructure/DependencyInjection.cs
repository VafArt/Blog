using Blog.CommentsService.Domain.Repositories;
using Blog.CommentsService.Infrastructure.Repositories;
using Blog.CommentsService.Infrastructure.TypeHandlers;
using Blog.Common.Infrastructure;
using Dapper;

namespace Blog.CommentsService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            SqlMapper.AddTypeHandler(new PostIdHandler());
            SqlMapper.AddTypeHandler(new CommentIdHandler());
            SqlMapper.AddTypeHandler(new UserIdHandler());

            services.AddSingleton<IDbInitializer, NpgsqlCommentsDbInitializer>();

            return services;
        }
    }
}
