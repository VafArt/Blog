using Blog.Common.CQRS.Decorators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
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
            services.AddScoped<ICommandDispatcher, CommandDispatcher>()
                .Decorate<ICommandDispatcher, ValidationCommandDispatcherDecorator>()
                .Decorate<ICommandDispatcher>((inner, provider) => 
                new LoggingCommandDispatcherDecorator(
                    inner, 
                    provider.GetRequiredService<ILogger<LoggingCommandDispatcherDecorator>>()));

            services.AddScoped<IQueryDispatcher, QueryDispatcher>()
                .Decorate<IQueryDispatcher, ValidationQueryDispatcherDecorator>()
                .Decorate<IQueryDispatcher>((inner, provider) =>
                new LoggingQueryDispatcherDecorator(
                    inner,
                    provider.GetRequiredService<ILogger<LoggingQueryDispatcherDecorator>>())); ;

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
