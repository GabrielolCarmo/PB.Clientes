using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB.Clientes.Domain.Clientes;

namespace PB.Clientes.Infra.Data.Clientes.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable(nameof(Cliente));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasColumnType("nvarchar(250)");

            builder.Property(x => x.Email)
                .HasColumnType("nvarchar(250)");

            builder.Property(x => x.Score);

            builder.Ignore(x => x.Events);
        }
    }
}
