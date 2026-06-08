using FacturacionBarberia.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacturacionBarberia.Infraestructure.Data
{
    public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
    {
        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
            builder.ToTable("Servicios");

            builder.HasKey(x => x.ServicioId);

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.Nombre)
                .IsUnique();

            builder.Property(x => x.Precio)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.Estado)
                .HasConversion<string>()
                .HasMaxLength(15)
                .IsRequired();

            builder.HasQueryFilter(x => !x.EstaEliminado);
        }
    }
}
