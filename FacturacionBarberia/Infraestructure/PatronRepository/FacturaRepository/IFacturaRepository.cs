using FacturacionBarberia.Domain.Models.Entities;

namespace FacturacionBarberia.Infraestructure.PatronRepository.FacturaRepository
{
    public interface IFacturaRepository
    {
        Task AddAsync(Factura factura);

        Task<Factura?> GetByIdAsync(int facturaId);

        Task<List<Factura>> GetAllAsync();
        Task<List<Factura>> GetActivasAsync();

        Task<Factura?> GetFacturaDetalleAsync(int facturaId);
    }
}
