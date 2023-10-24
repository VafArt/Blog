using Blog.Common.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Blog.Common.CQRS;
using Blog.Common.Application.Swagger;
using Blog.Common.Application;
using Microsoft.Extensions.Configuration;

namespace Blog.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommon(this IServiceCollection services, IConfiguration config)
        {
            services.AddCommonInfrastructure();

            services.AddCqrs();

            services.AddCommonApplication(config);

            return services;
        }
    }
}
