using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Common.Application.MessageBroker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonMessageBroker(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                busConfigurator.UsingRabbitMq((context, congifurator) =>
                {
                    congifurator.Host(new Uri(config["MessageBroker:Host"]!), h =>
                    {
                        h.Username(config["MessageBroker:Username"]!);
                        h.Password(config["MessageBroker:Password"]!);
                    });

                    congifurator.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
