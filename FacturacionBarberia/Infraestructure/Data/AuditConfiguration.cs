using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacturacionBarberia.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace FacturacionBarberia.Infraestructure.Data
{
    public static class AuditConfiguration
    {
        public static void ConfigureAudit<T>(
            EntityTypeBuilder<T> builder)
            where T : AuditoriaEntities
        {
            builder.Property(x => x.FechaCreacion)
                .IsRequired();

            builder.Property(x => x.UsuarioCreacion);

            builder.Property(x => x.FechaModificacion);

            builder.Property(x => x.UsuarioModificacion);

            builder.Property(x => x.FechaEliminacion);

            builder.Property(x => x.UsuarioEliminacion);

            builder.Property(x => x.EstaEliminado)
            .HasDefaultValue(false);
        }
    }
}
