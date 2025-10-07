using MediatR;
using PB.Clientes.Domain.Clientes;
using PB.Clientes.Domain.Clientes.Commands;
using PB.Clientes.Domain.Clientes.Services;
using PB.Clientes.Infra.Kernel.Application;
using PB.Clientes.Infra.Kernel.Domain;

namespace PB.Clientes.Application.CommandHandlers.Clientes
{
    public class CriarNovoClienteCommandHandler(IClienteRepository repository, IMediator mediator) : IRequestHandler<CriarNovoClienteCommand, IServiceOperationResult>
    {
        private readonly IClienteRepository _repository = repository;
        private readonly IMediator _mediator = mediator;

        public async Task<IServiceOperationResult> Handle(CriarNovoClienteCommand command, CancellationToken cancellationToken)
        {
            var result = new ServiceOperationResult();
            if (await _repository.EmailJaEstaCadastradoAsync(command.Email, cancellationToken))
            {
                return result.AddError(new NotificationError("CLI-ER-001", "O email já está cadastrado no sistema."));
            }

            var cliente = Cliente.Factory.CriarNovoCliente(command);

            await _repository.PersistirClienteAsync(cliente, cancellationToken);
            return result.PublishEvents(cliente, _mediator);
        }
    }
}