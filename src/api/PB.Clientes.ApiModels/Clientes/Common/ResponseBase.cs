namespace PB.Clientes.ApiModels.Clientes.Common
{
    public class ResponseBase
    {
        public readonly List<ErrorBase> Errors = [];

        public bool IsSuccess => Errors == null || Errors.Count > 0;
    }

    public class ErrorBase
    {
        public required string Key { get; set; }

        public required string Message { get; set; }
    }
}
