using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using PB.Clientes.Domain.Clientes.Events;
using PB.Commons.Api.Models;

namespace PB.Clientes.Application.EventHandler
{
    public class NovoClienteCriadoEventHandler(IBus bus, IConfiguration configuration) : INotificationHandler<NovoClienteCriadoEvent>
    {
        private readonly IBus _bus = bus;
        private readonly IConfiguration _configuration = configuration;

        public async Task Handle(NovoClienteCriadoEvent notification, CancellationToken cancellationToken)
        {
            var message = new NovoClienteCriadoMessage(notification.AggregateRootId, notification.Nome, notification.Email, notification.Score);

            var endPoint = await _bus.GetSendEndpoint(new Uri(_configuration["RabbitMQ:ClientesQueue"] ?? throw new InvalidOperationException("RabbitMQ:ClientesQueue configuration is missing.")));
            await endPoint.Send(message, cancellationToken);
        }
    }
}
