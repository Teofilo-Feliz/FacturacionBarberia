using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class ObtenerFacturaResponse
    {
        public int FacturaId { get; set; }

        public DateTime FechaFactura { get; set; }

        public string Cliente { get; set; } = string.Empty;

        public string Usuario { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public FormaPagoEnum FormaPago { get; set; } 

        public EstadoFacturaEnum EstadoFactura { get; set; } 
    }
}
