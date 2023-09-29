using Blog.Common.Domain.Repositories;
using Blog.Common.Infrastructure.TypeHandlers;
using Blog.Common.Infrastructure;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Common.CQRS;

namespace Blog.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddCommonInfrastructure();

            services.AddCqrs();

            return services;
        }
    }
}
