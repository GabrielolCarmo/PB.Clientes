namespace PB.Clientes.Infra.Kernel.Domain
{
    public abstract class AggregateRoot(Guid id)
    {
        public Guid Id { get; private protected set; } = id;

        public readonly List<IDomainEvent> Events = [];

        public void PublishEvent(IDomainEvent @event)
        {
            Events.Add(@event);
        }
    }
}
