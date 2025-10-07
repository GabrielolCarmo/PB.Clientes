using MediatR;
using PB.Clientes.Domain.Clientes;
using PB.Clientes.Domain.Clientes.Commands;
using PB.Clientes.Domain.Clientes.Services;
using PB.Commons.Infra.Kernel.Application;
using PB.Commons.Infra.Kernel.Domain;

namespace PB.Clientes.Application.CommandHandlers.Clientes
{
    /// <summary>
    /// Handler do comando para criar um novo cliente, realiza a orquestração entre os serviços de domínio.
    /// </summary>
    public class CriarNovoClienteCommandHandler(IClienteRepository repository, IMediator mediator) : IRequestHandler<CriarNovoClienteCommand, IServiceOperationResult>
    {
        private readonly IClienteRepository _repository = repository;
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Orquestra o processo de criação de um novo cliente.
        /// </summary>
        /// <param name="command">Comando de criação de cliente.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da operação de serviço.</returns>
        public async Task<IServiceOperationResult> Handle(CriarNovoClienteCommand command, CancellationToken cancellationToken)
        {
            var result = new ServiceOperationResult();
            if (await _repository.EmailJaEstaCadastradoAsync(command.Email, cancellationToken))
            {
                return result.AddError(new NotificationError("CLI-ER-001", "O email já está cadastrado no sistema."));
            }

            var cliente = Cliente.Factory.CriarNovoCliente(command);

            await _repository.PersistirClienteAsync(cliente, cancellationToken);
            return await result.PublishEvents(cliente, _mediator, cancellationToken);
        }
    }
}