namespace PB.Clientes.Domain.Clientes.Services
{
    /// <summary>
    /// Interface para repositório de clientes, as interfaces de repositório ficam na camada de dominio pois nada mais são do que serviços de dominio.
    /// </summary>
    public interface IClienteRepository
    {
    /// <summary>
    /// Verifica se o e-mail já está cadastrado.
    /// </summary>
    /// <param name="email">E-mail a ser verificado.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Verdadeiro se o e-mail já estiver cadastrado.</returns>
    public Task<bool> EmailJaEstaCadastradoAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Persiste um novo cliente no repositório.
    /// </summary>
    /// <param name="cliente">Cliente a ser persistido.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    public Task PersistirClienteAsync(Cliente cliente, CancellationToken cancellationToken);
    }
}
