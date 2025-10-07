using MediatR;
using PB.Clientes.Infra.Kernel.Application;

namespace PB.Clientes.Domain.Clientes.Commands
{
    public class CriarNovoClienteCommand : IRequest<IServiceOperationResult>
    {
        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required int Score { get; set; }
    }
}
