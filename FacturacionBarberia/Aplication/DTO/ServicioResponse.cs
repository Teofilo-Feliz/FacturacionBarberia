using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class ServicioResponse
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; } 
        public decimal Precio { get; set; }
        public EstadoEnum Estado { get; set; }
    }
}
