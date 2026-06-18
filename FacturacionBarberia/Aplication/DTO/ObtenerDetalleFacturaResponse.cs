using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class ObtenerDetalleFacturaResponse
    {
        public int FacturaId { get; set; }

        public string Cliente { get; set; } = string.Empty;

        public string Usuario { get; set; } = string.Empty;

        public DateTime FechaFactura { get; set; }

        public decimal Total { get; set; }

        public FormaPagoEnum FormaPago { get; set; }

        public EstadoFacturaEnum Estado { get; set; }

        public string? Observaciones { get; set; }

        public List<DetalleFacturaResponse> Detalles { get; set; }
            = new();
    }
}
