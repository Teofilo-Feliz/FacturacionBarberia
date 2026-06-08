using FacturacionBarberia.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacturacionBarberia.Infraestructure.Data
{
    public class DetalleFacturaConfiguration : IEntityTypeConfiguration<DetalleFactura>
    {
        public void Configure(EntityTypeBuilder<DetalleFactura> builder)
        {
            builder.ToTable("DetalleFacturas");

            builder.HasKey(x => x.DetalleFacturaId);

            builder.Property(x => x.Precio)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.Cantidad)
                .IsRequired();

            builder.HasOne(x => x.Factura)
                .WithMany(x => x.Detalles)
                .HasForeignKey(x => x.FacturaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Servicio)
                .WithMany()
                .HasForeignKey(x => x.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ignora el SubTotal calculado en C#
            builder.Ignore(x => x.SubTotal);
        }
    }
}
