using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;

namespace FacturacionBarberia.Aplication.Interfaces
{
    public interface ICliente
    {
        Task<Response<ClienteResponse>> AgregarCliente(ClienteRequest request);
        Task<Response<ObtenerClienteResponse>> ObtenerCliente (ObtenerClienteRequest request);
        Task<Response<EditarClienteRequest>> ObtenerClienteEditar(int id);
        Task<Response> EditarCliente(EditarClienteRequest request);
    }
}
