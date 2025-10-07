using MediatR;

namespace PB.Clientes.Infra.Kernel.Domain
{
    public interface IDomainEvent : INotification
    {
        public Guid AggregateRootId { get; }

        public string EventType { get; }
    }
}
