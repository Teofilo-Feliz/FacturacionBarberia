using FacturacionBarberia.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacturacionBarberia.Infraestructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        { 
        }
        public override async Task<int> SaveChangesAsync(
         CancellationToken cancellationToken = default)
        {
            AplicarAuditoria();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AplicarAuditoria()
        {
            var entradas = ChangeTracker
                .Entries<AuditoriaEntities>();

            foreach (var entry in entradas)
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.FechaCreacion = DateTime.UtcNow;
                       

                        break;

                    case EntityState.Modified:

                        entry.Entity.FechaModificacion = DateTime.UtcNow;

                        break;

                    
                }
            }
        }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }




    }
}
