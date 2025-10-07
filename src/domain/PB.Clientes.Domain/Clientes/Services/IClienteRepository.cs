namespace PB.Clientes.Domain.Clientes.Services
{
    public interface IClienteRepository
    {
        public Task<bool> EmailJaEstaCadastradoAsync(string email, CancellationToken cancellationToken);

        public Task PersistirClienteAsync(Cliente cliente, CancellationToken cancellationToken);
    }
}
