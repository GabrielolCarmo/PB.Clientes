using PB.Clientes.Domain.Clientes.Commands;
using PB.Clientes.Domain.Clientes.Events;
using PB.Commons.Infra.Kernel.Domain;

namespace PB.Clientes.Domain.Clientes
{
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

        public static class Factory
        {
            public static Cliente CriarNovoCliente(CriarNovoClienteCommand command)
            {
                return new Cliente(Guid.NewGuid(), command.Nome, command.Email, command.Score);
            }
        }
    }
}
