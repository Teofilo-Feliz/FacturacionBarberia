using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;

namespace FacturacionBarberia.Aplication.Interfaces
{
    public interface IServicio
    {
      Task<Response<ServicioResponse>> AgregarServicio(ServicioRequest request);
      Task<Response<ObtenerServicioResponse>> ObtenerServicios(ObtenerServicioRequest request);
      Task<Response<EditarServicesRequest>> ObtenerServicioEditar(int id);
      Task<Response> EditarServicio(EditarServicesRequest request);


    }
}
