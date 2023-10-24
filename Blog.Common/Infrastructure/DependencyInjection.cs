using Blog.Common.Domain.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Common.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                    throw new ApplicationException("The connection string is null");

                return new NpgsqlConnectionFactory(connectionString);
            });

            services.AddScoped<UnitOfWorkFactory>();
            services.AddScoped<IUnitOfWorkFactory>(x => x.GetRequiredService<UnitOfWorkFactory>());
            services.AddScoped<IDbConnectionProvider>(x => x.GetRequiredService<UnitOfWorkFactory>());

            return services;
        }
    }
}
