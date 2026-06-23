using Azure.Core;
using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionBarberia.Aplication.Controllers
{
    [Authorize]
    public class ServicioController : Controller
    {
        private readonly IServicio _servicio;

        public ServicioController(IServicio servicio)
        {
            _servicio = servicio;
        }
        [Authorize(Roles = "Cajero, Administrador")]
        [HttpGet]
        public IActionResult Crear ()
        {
            return View();
        }
        [Authorize(Roles = "Cajero, Administrador")]
        [HttpPost]
        public async Task<IActionResult> Crear(ServicioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);

            }
            var result = await _servicio.AgregarServicio(request);


            if (!result.Successful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View(request);

            }

            TempData["Success"] = "Servicio registrado correctamente.";

            return RedirectToAction(nameof(Crear));


        }
        [Authorize(Roles = "Cajero, Administrador")]
        [HttpGet]
        public async Task<IActionResult> ObtenerServicios(
        ConsultaViewModel<ObtenerServicioRequest, ObtenerServicioResponse> model)
        {
            var result = await _servicio.ObtenerServicios(model.Filtros);

            model.Resultados = result.DataList?.ToList()
                ?? new List<ObtenerServicioResponse>();

            if(!result.Successful)
            {
                TempData["Error"] = result.Message;
            }

            return View(model);

        }
        [Authorize(Roles = "Cajero, Administrador")]
        [HttpGet]
        public async Task<IActionResult> Editar(int id )
        {
            var result = await _servicio.ObtenerServicioEditar(id);
            if (!result.Successful)
            {
                TempData["Error"] =
                    result.Errors.FirstOrDefault()
                    ?? result.Message;
                return RedirectToAction(nameof(ObtenerServicios));
            }

            return View(result.Data);
        }
        [Authorize(Roles = "Cajero, Administrador")]
        [HttpPost]
        public async Task <IActionResult> Editar(EditarServicesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result =
                await _servicio.EditarServicio(request);

            if (!result.Successful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(request);
            }
            TempData["Success"] =
               "Services Actualizado correctamente.";
            return RedirectToAction(nameof(ObtenerServicios));


        }

    }
}
