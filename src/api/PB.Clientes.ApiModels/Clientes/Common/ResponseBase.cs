namespace PB.Clientes.ApiModels.Clientes.Common
{
    public class ResponseBase
    {
        public List<ErrorBase> Errors { get; } = [];

        public bool Success => Errors == null || Errors.Count <= 0;
    }

    public class ErrorBase
    {
        public required string Key { get; set; }

        public required string Message { get; set; }
    }
}
