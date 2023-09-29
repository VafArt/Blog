using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.CQRS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.TryAddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.TryAddSingleton<IQueryDispatcher, QueryDispatcher>();

            services.Scan(selector =>
            {
                selector.FromEntryAssembly()
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IQueryHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromEntryAssembly()
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(ICommandHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });

            return services;
        }
    }
}
