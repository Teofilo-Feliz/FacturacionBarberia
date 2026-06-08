using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Domain.Models.Entities
{
    public class Servicio: AuditoriaEntities
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public EstadoEnum Estado { get; set; }
        public ICollection<DetalleFactura> DetalleFacturas { get; set; }
       = new List<DetalleFactura>();

    }
}
