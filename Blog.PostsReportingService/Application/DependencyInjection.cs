using Blog.PostsReportingService.Application.Mappings;
using FluentValidation;
using System.Reflection;

namespace Blog.PostsReportingService.Application
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
