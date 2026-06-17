using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class UsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } 
        public string UserName { get; set; } 
        public RolEnum Rol { get; set; }
        public EstadoEnum Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? UsuarioCreacion {get; set; }
        public bool EstaEliminado { get; set; }


    }
}
