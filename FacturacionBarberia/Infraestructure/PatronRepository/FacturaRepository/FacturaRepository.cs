using FacturacionBarberia.Domain.Models.Entities;
using FacturacionBarberia.Domain.Models.Enum;
using FacturacionBarberia.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FacturacionBarberia.Infraestructure.PatronRepository.FacturaRepository
{
    public class FacturaRepository: IFacturaRepository
    {
        private readonly AppDbContext _context;

        public FacturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
        }

        public async Task<Factura?> GetByIdAsync(int facturaId)
        {
            return await _context.Facturas
                .FirstOrDefaultAsync(x => x.FacturaId == facturaId);
        }

        public async Task<List<Factura>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .OrderByDescending(x => x.FechaFactura)
                .ToListAsync();
        }

        public async Task<List<Factura>> GetActivasAsync()
        {
            return await _context.Facturas
                .Where(x => x.EstadoFactura == EstadoFacturaEnum.Activa)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .OrderByDescending(x => x.FechaFactura)
                .ToListAsync();
        }

        public async Task<Factura?> GetFacturaDetalleAsync(int facturaId)
        {
            return await _context.Facturas
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Include(x => x.Detalles)
                    .ThenInclude(x => x.Servicio)
                .FirstOrDefaultAsync(x => x.FacturaId == facturaId);
        }



    }
}
