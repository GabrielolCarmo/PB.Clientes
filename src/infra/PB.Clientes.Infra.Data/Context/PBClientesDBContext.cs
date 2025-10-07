using Microsoft.EntityFrameworkCore;
using PB.Clientes.Infra.Data.Clientes.Mapping;

namespace PB.Clientes.Infra.Data.Context
{
    public class PBClientesDBContext(DbContextOptions<PBClientesDBContext> opt) : DbContext(opt)
    {
        public readonly DbContextOptions<PBClientesDBContext> _opt = opt;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
