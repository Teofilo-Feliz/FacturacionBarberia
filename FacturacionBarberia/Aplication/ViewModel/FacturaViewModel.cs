using FacturacionBarberia.Aplication.DTO;
using FacturacionBarberia.Domain.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacturacionBarberia.Aplication.ViewModel
{
    public class FacturaViewModel
    {
        public int ClienteId { get; set; }

        public FormaPagoEnum FormaPago { get; set; }

        public string? Observaciones { get; set; }

        public List<DetalleFacturaRequest> Detalles { get; set; }
            = new();
        public int ServicioId { get; set; }

        public int Cantidad { get; set; } = 1;

        public List<SelectListItem> Clientes { get; set; }
            = new();

        public List<SelectListItem> Servicios { get; set; }
            = new();
    }
}
