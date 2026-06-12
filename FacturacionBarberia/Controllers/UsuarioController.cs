using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.Services;
using FacturacionBarberia.Aplication.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionBarberia.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuarioServices;

        public UsuarioController(IUsuario usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(
        UsuarioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result =
                await _usuarioServices.AgregarUsuario(request);

            if (!result.Successful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View(request);
            }

            TempData["Success"] =
                "Usuario registrado correctamente.";

            return RedirectToAction(nameof(Crear));
        }

        public async Task<IActionResult> ObtenerUsuarios(
         UsuarioConsultaViewModel model)
        {
            var result = await _usuarioServices
                .ObtenerUsuarios(model.Filtros);

            model.Usuarios = result.DataList?.ToList()
                ?? new List<ObtenerUsuarioResponse>();

            if (!result.Successful)
            {
                TempData["Error"] = result.Message;
            }

            return View(model);
        }

    }
}
