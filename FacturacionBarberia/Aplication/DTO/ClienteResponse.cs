using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class ClienteResponse
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; } 
        public string Correo { get; set; } 
        public DateTime FechaCreacion { get; set; }
        public EstadoEnum Estado { get; set; }

    }
}
