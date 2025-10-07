using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PB.Clientes.Application
{
    public static class ServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["Rabbit:Host"] ?? "localhost", "/", h =>
                    {
                        h.Username(configuration["Rabbit:User"] ?? "guest");
                        h.Password(configuration["Rabbit:Pass"] ?? "guest");
                    });
                });
            });
        }
    }
}
