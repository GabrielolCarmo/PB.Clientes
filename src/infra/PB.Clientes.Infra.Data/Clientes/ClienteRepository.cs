using Microsoft.EntityFrameworkCore;
using PB.Clientes.Domain.Clientes;
using PB.Clientes.Domain.Clientes.Services;
using PB.Clientes.Infra.Data.Context;

namespace PB.Clientes.Infra.Data.Clientes
{
    public class ClienteRepository(PBClientesDBContext context) : IClienteRepository
    {
        private readonly DbSet<Cliente>_dbSet = context.Set<Cliente>();

        public async Task PersistirClienteAsync(Cliente cliente, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(cliente, cancellationToken: cancellationToken);
        }

        public async Task<bool> EmailJaEstaCadastradoAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(c => c.Email == email, cancellationToken: cancellationToken);
        }
    }
}
