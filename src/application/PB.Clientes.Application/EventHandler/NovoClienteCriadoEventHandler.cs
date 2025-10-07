using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using PB.Clientes.Domain.Clientes.Events;
using PB.Commons.Api.Models;

namespace PB.Clientes.Application.EventHandler
{
    /// <summary>
    /// Manipulador de evento para quando um novo cliente é criado.
    /// Responsável por enviar a mensagem para fila RabbitMQ, realizando
    /// o mapping do evento para o tipo de mensagem compartilhada entre serviços,
    /// evitando assim ter que acoplar os dois projetos.
    /// </summary>
    public class NovoClienteCriadoEventHandler(IBus bus, IConfiguration configuration) : INotificationHandler<NovoClienteCriadoEvent>
    {
        private readonly IBus _bus = bus;
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Manipula o evento de novo cliente criado, enviando mensagem para a fila, já mapeada para o tipo de mensagem correspondente,
        /// utilizamos o GetSendEndpoint pois assim garantimos que mesmo que a fila não exista, ela será criada automaticamente.
        /// </summary>
        /// <param name="notification">Evento de novo cliente criado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        public async Task Handle(NovoClienteCriadoEvent notification, CancellationToken cancellationToken)
        {
            var message = new NovoClienteCriadoMessage(notification.AggregateRootId, notification.Nome, notification.Email, notification.Score);

            var endPoint = await _bus.GetSendEndpoint(new Uri(_configuration["RabbitMQ:ClientesQueue"] ?? throw new InvalidOperationException("RabbitMQ:ClientesQueue configuration is missing.")));
            await endPoint.Send(message, cancellationToken);
        }
    }
}
