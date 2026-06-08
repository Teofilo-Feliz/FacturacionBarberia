using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Domain.Models.Entities
{
    public class Factura: AuditoriaEntities
    {
        public int FacturaId { get; set; }

        public DateTime FechaFactura { get; set; }

        public int ClienteId { get; set; }

        public int UsuarioId { get; set; }

        public decimal Total { get; set; }

        public FormaPagoEnum FormaPago { get; set; }

        public string? Observaciones { get; set; }

        public Cliente Cliente { get; set; } = null!;

        public Usuario Usuario { get; set; } = null!;

        public ICollection<DetalleFactura> Detalles { get; set; }
            = new List<DetalleFactura>();
    }
}
