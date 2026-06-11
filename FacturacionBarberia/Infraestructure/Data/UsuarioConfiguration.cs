using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Domain.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacturacionBarberia.Infraestructure.Data
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.UsuarioId);

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Rol)
                .HasConversion(
                v => v.ToString(),                    
                v => (RolEnum)Enum.Parse(typeof(RolEnum), v)
                )
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Estado)
                .HasConversion(
                v => v.ToString(),                    
                v => (EstadoEnum)Enum.Parse(typeof(EstadoEnum), v)
                )
                .HasMaxLength(15)
                .IsRequired();

            builder.HasQueryFilter(x => !x.EstaEliminado);
        }
    }
}
