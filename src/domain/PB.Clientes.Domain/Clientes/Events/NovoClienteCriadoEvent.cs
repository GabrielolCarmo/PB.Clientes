using PB.Commons.Infra.Kernel.Domain;

namespace PB.Clientes.Domain.Clientes.Events
{
    public class NovoClienteCriadoEvent(Cliente cliente) : IDomainEvent
    {
        public Guid AggregateRootId { get; } = cliente.Id;

        public string EventType => "NOVO_CLIENTE_CRIADO";

        public string Nome { get; } = cliente.Nome;

        public string Email { get; } = cliente.Email;

        public int Score { get; } = cliente.Score;
    }
}
