using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Helpers;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Infraestructure.PatronRepository.DashboardRepository;

namespace FacturacionBarberia.Aplication.Services
{
    public class DashboardServices:IDashboard
    {
        private readonly IDashboardRepository _repository;

        public DashboardServices (IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DashboardObtenerResumen>> ObtenerResumen() 
        {
            return await _repository.ObtenerResumen();

        }

        public async Task<List<DashboardObtenerIngresosMeses>> ObtenerIngresosMeses()
        {
            return await _repository.ObtenerIngresosMeses();
        }

        public async Task<List<DashboardObtenerIngresosSemana>> ObtenerIngresosSemanas()
        {
            return await _repository.ObtenerIngresosSemana();
        }

        public async Task<List<DashboardObtenerTopServicios>> ObtenerTopServicio()
        {
            return await _repository.ObtenerTopServicios();
        }
    }
}
