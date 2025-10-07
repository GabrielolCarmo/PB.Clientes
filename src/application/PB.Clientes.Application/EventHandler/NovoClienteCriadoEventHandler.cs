using MediatR;
using PB.Clientes.Domain.Clientes.Events;

namespace PB.Clientes.Application.EventHandler
{
    public class NovoClienteCriadoEventHandler : INotificationHandler<NovoClienteCriadoEvent>
    {
        public async Task Handle(NovoClienteCriadoEvent notification, CancellationToken cancellationToken)
        {
            // Implementar a lógica de manipulação do evento aqui
            await Task.CompletedTask;
        }
    }
}
