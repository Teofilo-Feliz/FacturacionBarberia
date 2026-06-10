using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Infraestructure.Audit;
using Microsoft.EntityFrameworkCore;

namespace FacturacionBarberia.Infraestructure.Data
{
    public class AppDbContext: DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService)
       : base(options)
        { 
            _currentUserService = currentUserService;
        }
        public override async Task<int> SaveChangesAsync(
         CancellationToken cancellationToken = default)
        {
            AplicarAuditoria();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AplicarAuditoria()
        {
            var usuarioId = _currentUserService.UsuarioId;

            var entradas =
                ChangeTracker.Entries<AuditoriaEntities>();

            foreach (var entry in entradas)
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.FechaCreacion =
                            DateTime.UtcNow;

                        entry.Entity.UsuarioCreacion =
                            usuarioId;

                        break;

                    case EntityState.Modified:

                        entry.Entity.FechaModificacion =
                            DateTime.UtcNow;

                        entry.Entity.UsuarioModificacion =
                            usuarioId;

                        break;

                    case EntityState.Deleted:

                        entry.State = EntityState.Modified;

                        entry.Entity.EstaEliminado = true;

                        entry.Entity.FechaEliminacion =
                            DateTime.UtcNow;

                        entry.Entity.UsuarioEliminacion =
                            usuarioId;

                        break;
                }
            }
        }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }

        protected override void OnModelCreating(
        ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasQueryFilter(x => !x.EstaEliminado);

            modelBuilder.Entity<Cliente>()
                .HasQueryFilter(x => !x.EstaEliminado);

            modelBuilder.Entity<Servicio>()
                .HasQueryFilter(x => !x.EstaEliminado);

            modelBuilder.Entity<Factura>()
                .HasQueryFilter(x => !x.EstaEliminado);
        }



    }
}
