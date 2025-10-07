using PB.Clientes.Infra.Kernel.Domain;

namespace PB.Clientes.Infra.Kernel.Application
{
    public interface IServiceOperationResult
    {
        public IServiceOperationResult AddError(NotificationError error);

        public bool IsSuccess { get; }

        public bool IsFailure { get; }

        public List<NotificationError> Errors { get; }
    }

    public class ServiceOperationResult : IServiceOperationResult
    {
        public bool IsSuccess => Errors.Count == 0;

        public bool IsFailure => !IsSuccess;

        public List<NotificationError> Errors { get; } = [];

        public IServiceOperationResult AddError(NotificationError error)
        {
            Errors.Add(error);

            return this;
        }

        public IServiceOperationResult PublishEvents(AggregateRoot aggregate)
        {
            return this;
        }
    }
}
