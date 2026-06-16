using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Domain.Models.Entities
{
    public class Cliente: AuditoriaEntities
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public EstadoEnum Estado { get; set; }

        public ICollection<Factura> Facturas { get; set; }
         = new List<Factura>();

    }
}
