using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PB.Clientes.ApiModels.Clientes;
using PB.Clientes.ApiModels.Clientes.Common;

namespace PB.Clientes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType<CriarNovoCliente.Response>(StatusCodes.Status201Created, "application/json")]
        [ProducesResponseType<CriarNovoCliente.Response>(StatusCodes.Status400BadRequest, "application/json")]
        [ProducesResponseType<CriarNovoCliente.Response>(StatusCodes.Status500InternalServerError, "application/json")]
        public async Task<IActionResult> CriarCliente(CriarNovoCliente.Request request, CancellationToken cancellationToken)
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
                return Ok(apiResponse);
            }

            apiResponse.Errors.AddRange(result.Errors.Select(e => new ErrorBase
            {
                Key = e.Key,
                Message = e.Message,
            }));

            return BadRequest(apiResponse);
        }

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
