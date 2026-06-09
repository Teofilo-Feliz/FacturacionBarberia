using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class UsuarioRequest
    {
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RolEnum Rol { get; set; }
        public EstadoEnum Estado { get; set; }


    }
}
