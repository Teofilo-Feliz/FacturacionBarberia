using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace FacturacionBarberia.Aplication.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ICliente _cliente;

        public ClienteController(ICliente cliente)
        {
            _cliente = cliente;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ClienteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _cliente.AgregarCliente(request);

            if (!result.Successful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View(request);
            }

            TempData["Success"] = "Cliente registrado correctamente.";

            return RedirectToAction(nameof(Crear));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerClientes(
            ConsultaViewModel<ObtenerClienteRequest, ObtenerClienteResponse> model)
        {
            var result = await _cliente.ObtenerCliente(model.Filtros);

            model.Resultados = result.DataList?.ToList()
                ?? new List<ObtenerClienteResponse>();

            if (!result.Successful)
            {
                TempData["Error"] = result.Message;
            }

            return View("~/Views/Cliente/ObtenerClientes.cshtml", model);

        }
    }
}
