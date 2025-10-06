using PB.Clientes.Infra.Kernel.Domain;

namespace PB.Clientes.Domain.Clientes.Events
{
    public class NovoClienteCriadoEvent : IDomainEvent
    {
        public Guid AggregateRootId => throw new NotImplementedException();

        public string EventType => "NOVO_CLIENTE_CRIADO";
    }
}
