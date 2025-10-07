using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PB.Clientes.ApiModels.Clientes;
using PB.Commons.Api;
using PB.Commons.Infra.Kernel.Data;

namespace PB.Clientes.Api.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController(IMediator mediator, IUnityOfWork uow) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IUnityOfWork _uow = uow;

        /// <summary>
        /// Cria um novo cliente no sistema.
        /// </summary>
        /// <param name="request">Dados para criação do cliente.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Retorna o resultado da operação de criação do cliente, no padrão do CriarNovoCliente.Response.</returns>
        [HttpPost]
        [ProducesResponseType<CriarNovoCliente.Response>(StatusCodes.Status201Created, "application/json")]
        [ProducesResponseType<CriarNovoCliente.Response>(StatusCodes.Status400BadRequest, "application/json")]
        [ProducesResponseType<CriarNovoCliente.Response>(StatusCodes.Status500InternalServerError, "application/json")]
        public async Task<IActionResult> CriarCliente(CriarNovoCliente.Request request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BuildResultWithInvalidModelState(ModelState);
                }

                var command = request.ToCommand();
                var result = await _mediator.Send(command, cancellationToken);

                var apiResponse = new CriarNovoCliente.Response();
                if (result.IsSuccess)
                {
                    await _uow.CommitTransactionAsync(cancellationToken);
                    return Created(string.Empty, apiResponse);
                }

                apiResponse.Errors.AddRange(result.Errors.Select(e => new ErrorBase
                {
                    Key = e.Key,
                    Message = e.Message,
                }));

                return BadRequest(apiResponse);
            }
            catch (Exception)
            {
                return BuildInternalServerError();
            }
        }

        /// <summary>
        /// Retorna resposta de erro interno do servidor no padrão do ResponseBase.
        /// </summary>
        private static ObjectResult BuildInternalServerError()
        {
            var response = new CriarNovoCliente.Response();
            response.Errors.Add(new ErrorBase()
            {
                Key = "CLI-ER-500",
                Message = "Ocorreu um erro inesperado no servidor. Tente novamente mais tarde."
            });

            return new ObjectResult(response);
        }

        /// <summary>
        /// Retorna resposta de erro de validação do ModelState no padrão do ResponseBase.
        /// </summary>
        /// <param name="modelState">ModelState inválido.</param>
        private static BadRequestObjectResult BuildResultWithInvalidModelState(ModelStateDictionary modelState)
        {
            var result = Activator.CreateInstance<ResponseBase>();
            result.Errors.AddRange(modelState.SelectMany(x => x.Value?.Errors.Select(e => new ErrorBase()
            {
                Key = x.Key,
                Message = e.ErrorMessage,
            }) ?? []));

            return new BadRequestObjectResult(result);
        }
    }
}
