using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionBarberia.Aplication.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashboard;
        
        public DashboardController(IDashboard dashboard)
        {
            _dashboard = dashboard;
        }


        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                Resumen = (await _dashboard.ObtenerResumen()).FirstOrDefault(),
                IngresosSemana = (await _dashboard.ObtenerIngresosSemanas()),
                IngresosMeses = (await _dashboard.ObtenerIngresosMeses()),
                TopServicios = (await _dashboard.ObtenerTopServicio()),
            };
            return View(model);
        }

    }
}
