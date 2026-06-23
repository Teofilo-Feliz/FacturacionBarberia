using FacturacionBarberia.Aplication.DTO;

namespace FacturacionBarberia.Aplication.ViewModel
{
    public class DashboardViewModel
    {
        public DashboardObtenerResumen Resumen { get; set; }

        public List<DashboardObtenerIngresosSemana> IngresosSemana { get; set; }

        public List<DashboardObtenerIngresosMeses> IngresosMeses { get; set; }

        public List<DashboardObtenerTopServicios> TopServicios { get; set; }
    }
}
