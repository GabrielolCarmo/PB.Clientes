using PB.Clientes.Domain.Clientes.Commands;
using PB.Clientes.Domain.Clientes.Events;
using PB.Commons.Infra.Kernel.Domain;

namespace PB.Clientes.Domain.Clientes
{
    /// <summary>
    /// Raiz de agregação que representa um cliente, deve centralizar as complexidades de negocio do cliente, no seu ciclo de vida.
    /// </summary>
    public class Cliente : AggregateRoot
    {
        private Cliente(Guid id, string nome, string email, int score) : base(id)
        {
            Nome = nome;
            Email = email;
            Score = score;

            PublishEvent(new NovoClienteCriadoEvent(this));
        }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public int Score { get; private set; }

        /// <summary>
        /// Fábrica para criação de instâncias de Cliente.
        /// </summary>
        public static class Factory
        {
            /// <summary>
            /// Cria uma nova instância válida de Cliente a partir do comando CriarNovoClienteCommand.
            /// </summary>
            /// <param name="command">Comando de criação de cliente.</param>
            /// <returns>Nova instância de Cliente.</returns>
            public static Cliente CriarNovoCliente(CriarNovoClienteCommand command)
            {
                return new Cliente(Guid.NewGuid(), command.Nome, command.Email, command.Score);
            }
        }
    }
}
