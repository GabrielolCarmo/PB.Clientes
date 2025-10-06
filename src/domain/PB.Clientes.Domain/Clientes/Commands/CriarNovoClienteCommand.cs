namespace PB.Clientes.Domain.Clientes.Commands
{
    public class CriarNovoClienteCommand
    {
        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required int Score { get; set; }
    }
}
