using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Aplication.DTO
{
    public class EditarUsuarioRequest
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; }

        public RolEnum Rol { get; set; }

        public EstadoEnum Estado { get; set; }
    }
}
