using PB.Clientes.Infra.Data.Context;
using PB.Commons.Infra.Kernel.Data;

namespace PB.Clientes.Infra.Data
{
    /// <summary>
    /// O UOW garante que no ciclo de vida de uma operação apenas uma transação deve ser feita, caso algo dê errado, todas as alterações devem ser revertidas
    /// Poderiamos ter implementado o BeginTransaction, mas como o Entity Framework já gerencia isso optei por não implementar, mesmo que um cenario real seria
    /// melhor realizar essa implementação mesmo com um metodo vazio, pois diminui o custo para mudanças.
    /// </summary>
    public class UnityOfWork(PBClientesDBContext context) : IUnityOfWork
    {
        private readonly PBClientesDBContext _context = context;

        /// <summary>
        /// Realiza o commit da transação no contexto do banco de dados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Verdadeiro se houver alterações persistidas.</returns>
        public async Task<bool> CommitTransactionAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
