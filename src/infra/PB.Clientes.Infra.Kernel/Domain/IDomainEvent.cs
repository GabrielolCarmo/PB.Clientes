namespace PB.Clientes.Infra.Kernel.Domain
{
    public interface IDomainEvent
    {
        public Guid AggregateRootId { get; }

        public string EventType { get; }
    }
}
