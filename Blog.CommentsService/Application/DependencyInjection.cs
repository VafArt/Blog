using Blog.CommentsService.Application.Mappings;
using FluentValidation;
using System.Reflection;

namespace Blog.CommentsService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ICommentMapper, CommentMapper>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
