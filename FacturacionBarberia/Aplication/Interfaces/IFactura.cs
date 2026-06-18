using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;

namespace FacturacionBarberia.Aplication.Interfaces
{
    public interface IFactura
    {
        Task<Response<ObtenerDetalleFacturaResponse>> AgregarFacturas(FacturaRequest request);
        Task<Response<ObtenerFacturaResponse>> ObtenerFactura(ObtenerFacturaRequest request);
        Task<Response<ObtenerDetalleFacturaResponse>> ObtenerFacturaDetalle (int facturaId);
        Task<Response<bool>> AnularFactura(int facturaId);



    }
}
