using Microsoft.EntityFrameworkCore;
using PB.Clientes.Domain.Clientes;
using PB.Clientes.Domain.Clientes.Services;
using PB.Clientes.Infra.Data.Context;

namespace PB.Clientes.Infra.Data.Clientes
{
    /// <summary>
    /// Repositório de acesso a dados para entidade Cliente, implementação concreta.
    /// </summary>
    public class ClienteRepository(PBClientesDBContext context) : IClienteRepository
    {
        private readonly DbSet<Cliente> _dbSet = context.Set<Cliente>();

        /// <summary>
        /// Persiste um novo cliente no banco de dados.
        /// </summary>
        /// <param name="cliente">Cliente a ser persistido.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        public async Task PersistirClienteAsync(Cliente cliente, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(cliente, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Verifica se o e-mail já está cadastrado no banco de dados.
        /// </summary>
        /// <param name="email">E-mail a ser verificado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Verdadeiro se o e-mail já estiver cadastrado.</returns>
        public async Task<bool> EmailJaEstaCadastradoAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(c => c.Email == email, cancellationToken: cancellationToken);
        }
    }
}
