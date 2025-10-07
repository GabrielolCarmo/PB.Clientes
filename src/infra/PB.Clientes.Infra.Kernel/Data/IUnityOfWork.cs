namespace PB.Clientes.Infra.Kernel.Data
{
    public interface IUnityOfWork
    {
        public Task<bool> CommitTransactionAsync(CancellationToken cancellationToken);
    }
}
