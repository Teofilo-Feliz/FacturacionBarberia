using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FacturacionBarberia.Aplication.Controllers
{
    [Authorize(Roles = "Cajero, Administrador")]
    public class ClienteController : Controller
    {
        private readonly ICliente _cliente;

        public ClienteController(ICliente cliente)
        {
            _cliente = cliente;
        }

        [Authorize(Roles = "Cajero, Administrador")]
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [Authorize(Roles = "Cajero, Administrador")]
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
                    ModelState.AddModelError("", error);
                }

                return View(request);
            }

            TempData["Success"] = "Cliente registrado correctamente.";

            return RedirectToAction(nameof(Crear));
        }
        [Authorize(Roles = "Cajero, Administrador")]
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
        [Authorize(Roles = "Cajero, Administrador")]
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var result = await _cliente.ObtenerClienteEditar(id);
            if (!result.Successful)
            {
                TempData["Error"] =
                    result.Errors.FirstOrDefault()
                    ?? result.Message;
                return RedirectToAction(nameof(ObtenerClientes));
            }

            return View(result.Data);
        }
        [Authorize(Roles = "Cajero, Administrador")]
        [HttpPost]
        public async Task<IActionResult> Editar(EditarClienteRequest request)
        {
            if (!ModelState.IsValid)
            {
               return View(request);
            }

            var result =
                await _cliente.EditarCliente(request);

            if (!result.Successful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(request);
            }

            TempData["Success"] =
                "Cliente Actualizado correctamente.";
            return RedirectToAction(nameof(ObtenerClientes));
        }

    }
}
