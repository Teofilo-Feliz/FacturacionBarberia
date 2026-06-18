using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class FacturaRequest
    {
        public int ClienteId { get; set; }
        public FormaPagoEnum FormaPago { get; set; }
        public string? Observaciones { get; set; }
        public List<DetalleFacturaRequest> Detalles { get; set; }
        = new();


    }
}
