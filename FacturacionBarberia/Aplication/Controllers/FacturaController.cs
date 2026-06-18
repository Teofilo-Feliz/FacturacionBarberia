using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionBarberia.Aplication.Controllers
{
    public class FacturaController : Controller
    {
        private readonly IFactura _factura;

        public FacturaController(IFactura factura)
        {
            _factura = factura;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(ObtenerFacturas));
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(FacturaRequest request)
        {
            var result = await _factura.AgregarFacturas(request);

            if (!result.Successful)
            {
                TempData["Error"] = result.Message;
                return View(request);
            }

            TempData["Success"] = result.Message;

            return RedirectToAction(nameof(Crear));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerFacturas(
      ConsultaViewModel<ObtenerFacturaRequest,
      ObtenerFacturaResponse> model)
        {
            var result = await _factura.ObtenerFactura(model.Filtros);

            model.Resultados = result.DataList?.ToList()
                ?? new List<ObtenerFacturaResponse>();

            if (!result.Successful)
            {
                TempData["Error"] = result.Message;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int facturaId)
        {
            var result =
                await _factura.ObtenerFacturaDetalle(facturaId);

            if (!result.Successful)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction(nameof(ObtenerFacturas));
            }

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Anular(int facturaId)
        {
            var result =
                await _factura.AnularFactura(facturaId);

            TempData[result.Successful
                ? "Success"
                : "Error"] = result.Message;

            return RedirectToAction(nameof(ObtenerFacturas));
        }

    }
}
