using FacturacionBarberia.Aplication.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacturacionBarberia.Aplication.ViewModel
{
    public class CrearFacturaViewModel
    {
        public FacturaRequest Factura { get; set; }
       = new();

        public List<SelectListItem> Clientes { get; set; }
            = new();

        public List<SelectListItem> Servicios { get; set; }
            = new();
    }
}
