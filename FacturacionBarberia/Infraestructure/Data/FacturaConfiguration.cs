using FacturacionBarberia.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacturacionBarberia.Infraestructure.Data
{
    public class FacturaConfiguration : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder.ToTable("Facturas");

            builder.HasKey(x => x.FacturaId);

            builder.Property(x => x.Total)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.FormaPago)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Observaciones)
                .HasMaxLength(500);

            builder.Property(x => x.EstadoFactura)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Facturas)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Facturas)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.EstaEliminado);
        }
    }
}
