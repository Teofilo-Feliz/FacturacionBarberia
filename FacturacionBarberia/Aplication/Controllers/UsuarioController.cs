using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.Services;
using FacturacionBarberia.Aplication.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionBarberia.Aplication.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuarioServices;

        public UsuarioController(IUsuario usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        public async Task<IActionResult> Index(
          ConsultaViewModel<ObtenerUsuarioRequest, ObtenerUsuarioResponse> model)
        {
            return await ObtenerUsuarios(model);
        }
        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ObtenerUsuarios(
     ConsultaViewModel<ObtenerUsuarioRequest, ObtenerUsuarioResponse> model)
        {
            var result = await _usuarioServices
                .ObtenerUsuarios(model.Filtros);

            model.Resultados = result.DataList?.ToList()
                ?? new List<ObtenerUsuarioResponse>();

            if (!result.Successful)
            {
                TempData["Error"] = result.Message;
            }

            return View(model);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var result =
                await _usuarioServices.ObtenerUsuarioEditar(id);

            if (!result.Successful)
            {
                TempData["Error"] =
                    result.Errors.FirstOrDefault()
                    ?? result.Message;

                return RedirectToAction(nameof(ObtenerUsuarios));
            }

            return View(result.Data);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Editar(
         EditarUsuarioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result =
                await _usuarioServices.EditarUsuario(request);

            if (!result.Successful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View(request);
            }

            TempData["Success"] =
                "Usuario actualizado correctamente.";

            return RedirectToAction(nameof(ObtenerUsuarios));
        }

    }
}
