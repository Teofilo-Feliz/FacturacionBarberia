using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Aplication.Interfaces;
using FacturacionBarberia.Aplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacturacionBarberia.Aplication.Controllers
{
    public class FacturaController : Controller
    {
        private readonly IFactura _factura;
        private readonly ICliente _cliente;
        private readonly IServicio _servicio;

        public FacturaController(IFactura factura, ICliente cliente, IServicio servicio )
        {
            _factura = factura;
            _cliente = cliente;
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var model = new CrearFacturaViewModel();

            await CargarCombos(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearFacturaViewModel model)
        {
            var result = await _factura.AgregarFacturas(model.Factura);

            if (!result.Successful)
            {
                TempData["Error"] = result.Message;

                
                await CargarCombos(model);

                return View(model);
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

        private async Task CargarCombos(
        CrearFacturaViewModel model)
        {
            var clientes =
                await _cliente.ObtenerCliente(
                    new ObtenerClienteRequest());

            var servicios =
                await _servicio.ObtenerServicios(
                    new ObtenerServicioRequest());

            model.Clientes =
                clientes.DataList?
                .Select(x => new SelectListItem
                {
                    Value = x.ClienteId.ToString(),
                    Text = x.Nombre
                })
                .ToList()
                ?? new List<SelectListItem>();

            model.Servicios =
                servicios.DataList?
                .Select(x => new SelectListItem
                {
                    Value = x.ServicioId.ToString(),
                    Text = $"{x.Nombre} - RD$ {x.Precio:N2}"
                })
                .ToList()
                ?? new List<SelectListItem>();

            model.ServiciosData =
            servicios.DataList?.ToList()
            ?? new List<ObtenerServicioResponse>();


        }

    }
}
