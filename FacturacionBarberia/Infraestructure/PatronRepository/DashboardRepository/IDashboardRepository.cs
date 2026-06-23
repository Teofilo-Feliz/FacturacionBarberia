using FacturacionBarberia.Aplication.DTO;

namespace FacturacionBarberia.Infraestructure.PatronRepository.DashboardRepository
{
    public interface IDashboardRepository
    {
        Task<List<DashboardObtenerResumen>> ObtenerResumen();
        Task<List<DashboardObtenerIngresosMeses>> ObtenerIngresosMeses();
        Task<List<DashboardObtenerIngresosSemana>> ObtenerIngresosSemana();
        Task<List<DashboardObtenerTopServicios>> ObtenerTopServicios();

    }
}
