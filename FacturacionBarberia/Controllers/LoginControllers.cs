using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FacturacionBarberia.Controllers
{
    public class LoginControllers : Controller
    {
        private readonly IUsuario _usuarioServices;

        public LoginControllers(IUsuario usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _usuarioServices.Login(request);

            if (!result.Successful)
            {
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                else
                {
                    ModelState.AddModelError(
                        string.Empty,
                        result.Message);
                }

                return View(request);
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    result.Data!.UsuarioId.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    result.Data.Nombre),

                new Claim(
                    ClaimTypes.Role,
                    result.Data.Rol.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction(
                "Index",
                "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(
                "Index",
                "Login");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}


