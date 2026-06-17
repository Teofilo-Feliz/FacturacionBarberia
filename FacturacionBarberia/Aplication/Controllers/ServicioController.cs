using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionBarberia.Aplication.Controllers
{
    public class ServicioController : Controller
    {
        private readonly IServicio _servicio;

        public ServicioController(IServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult Crear ()
        {
            return View();
        }

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

    }
}
