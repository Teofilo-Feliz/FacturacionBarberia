using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FacturacionBarberia.Infraestructure.PatronRepository.DashboardRepository
{
    public class DashboardRepository: IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DashboardObtenerResumen>> ObtenerResumen()
        { 
            return await _context
                .Set<DashboardObtenerResumen>()
                .FromSqlRaw("EXEC Dashboard_ObtenerResumen")
                .ToListAsync();
        }

        public async Task<List<DashboardObtenerIngresosMeses>> ObtenerIngresosMeses()
        {
            return await _context
                .Set<DashboardObtenerIngresosMeses>()
                .FromSqlRaw("EXEC Dashboard_ObtenerIngresosMeses")
                .ToListAsync();
        }

        public async Task<List<DashboardObtenerIngresosSemana>> ObtenerIngresosSemana()
        {
            return await _context
                .Set<DashboardObtenerIngresosSemana>()
                .FromSqlRaw("EXEC Dashboard_ObtenerIngresosSemana")
                .ToListAsync();
        }

        public async Task<List<DashboardObtenerTopServicios>> ObtenerTopServicios()
        {
            return await _context
                .Set<DashboardObtenerTopServicios>()
                .FromSqlRaw("EXEC Dashboard_ObtenerTopServicios")
                .ToListAsync();
        }


    }
}
