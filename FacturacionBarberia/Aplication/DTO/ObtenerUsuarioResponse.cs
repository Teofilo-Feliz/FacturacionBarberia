using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class ObtenerUsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public RolEnum Rol { get; set; }
        public EstadoEnum Estado { get; set; } 
        
    }
}
