using Blog.Common.Application.Auth;
using Blog.Common.Application.MessageBroker;
using Blog.Common.Application.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddCommonSwagger();

            services.AddCommonAuth(config);

            services.AddCommonMessageBroker(config);

            return services;
        }
    }
}
