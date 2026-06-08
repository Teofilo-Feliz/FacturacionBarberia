using FacturacionBarberia.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FacturacionBarberia.Infraestructure.Data
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.ClienteId);

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Telefono)
                .HasMaxLength(20);

            builder.HasIndex(x => x.Telefono)
                .IsUnique();

            builder.Property(x => x.Correo)
                .HasMaxLength(100);

            builder.HasIndex(x => x.Correo)
                .IsUnique();

            builder.Property(x => x.Estado)
                .HasConversion<string>()
                .HasMaxLength(15)
                .IsRequired();

            builder.HasQueryFilter(x => !x.EstaEliminado);
        }
    }
}
