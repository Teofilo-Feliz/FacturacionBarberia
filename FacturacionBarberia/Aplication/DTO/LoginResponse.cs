using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class LoginResponse
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public RolEnum Rol { get; set; }

    }
}
