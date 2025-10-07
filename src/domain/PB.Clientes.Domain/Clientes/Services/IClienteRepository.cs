namespace PB.Clientes.Domain.Clientes.Services
{
    public interface IClienteRepository
    {
        public Task<bool> ValidaSeEmailEstaCadastradoAsync(string email, CancellationToken cancellationToken);

        public Task PersistirClienteAsync(Cliente cliente, CancellationToken cancellationToken);
    }
}
