using PB.Clientes.Domain.Clientes.Commands;
using PB.Commons.Api;
using System.ComponentModel.DataAnnotations;

namespace PB.Clientes.ApiModels.Clientes
{
    public class CriarNovoCliente
    {
        public class Request
        {
            [Required(ErrorMessage = "O Nome deve ser informado.")]
            public string? Nome { get; set; }

            [Required(ErrorMessage = "O Email deve ser informado")]
            [EmailAddress(ErrorMessage = "Deve ser informado um e-mail válido.")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "O Score deve ser informado")]
            [Range(0, 1000, ErrorMessage = "O valor do Score deve estar entre 0 e 1000.")]
            public int? Score { get; set; }

            public CriarNovoClienteCommand ToCommand()
            {
                return new CriarNovoClienteCommand
                {
                    Email = Email!,
                    Nome = Nome!,
                    Score = Score!.Value
                };
            }
        }

        public class Response : ResponseBase
        {
        }
    }
}
