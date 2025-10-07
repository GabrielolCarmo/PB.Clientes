namespace PB.Clientes.Infra.Kernel.Domain
{
    public class ErrorType(int errorType)
    {
        public readonly int Code = errorType;

        public static readonly ErrorType Validation = new(400);

        public static readonly ErrorType NotFound = new(404);
    }

    public record NotificationError
    {
        public NotificationError(string key, string message)
        {
            Key = key;
            Message = message;
            ErrorType = ErrorType.Validation;
        }

        public NotificationError(string key, string message, ErrorType errorType) : this (key, message)
        {
            ErrorType = errorType;
        }

        public string Key { get; }

        public string Message { get; }

        public ErrorType ErrorType { get; }
    }
}
