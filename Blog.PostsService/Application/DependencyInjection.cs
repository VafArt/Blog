using Blog.PostsService.Application.Mappings;
using Blog.PostsService.Application.Users.Created;
using FluentValidation;
using MassTransit;
using System.Reflection;

namespace Blog.PostsService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSingleton<IPostMapper, PostMapper>();

            return services;
        }
    }
}
