using PB.Clientes.Infra.Data.Context;
using PB.Commons.Infra.Kernel.Data;

namespace PB.Clientes.Infra.Data
{
    public class UnityOfWork(PBClientesDBContext context) : IUnityOfWork
    {
        private readonly PBClientesDBContext _context = context;

        public async Task<bool> CommitTransactionAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
