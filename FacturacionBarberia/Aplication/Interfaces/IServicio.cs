using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;

namespace FacturacionBarberia.Aplication.Interfaces
{
    public interface IServicio
    {
      Task<Response<ServicioResponse>> AgregarServicio(ServicioRequest request);


    }
}
