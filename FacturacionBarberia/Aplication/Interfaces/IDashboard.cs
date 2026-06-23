using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;

namespace FacturacionBarberia.Aplication.Interfaces
{
    public interface IDashboard
    {
        Task<List<DashboardObtenerResumen>> ObtenerResumen();
        Task<List<DashboardObtenerIngresosMeses>> ObtenerIngresosMeses();
        Task<List<DashboardObtenerIngresosSemana>> ObtenerIngresosSemanas();
        Task<List<DashboardObtenerTopServicios>> ObtenerTopServicio();
    }
}
