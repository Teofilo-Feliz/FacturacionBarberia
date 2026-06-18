using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class ObtenerFacturaRequest
    {
        public int? FacturaId { get; set; }

        public string? Cliente { get; set; }

        public string? Usuario { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        public FormaPagoEnum? FormaPago { get; set; }

        public EstadoFacturaEnum? EstadoFactura { get; set; }
    }
}
