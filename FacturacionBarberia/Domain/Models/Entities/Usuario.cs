using FacturacionBarberia.Domain.Models.Enum;

namespace FacturacionBarberia.Domain.Models.Entities
{
    public class Usuario: AuditoriaEntities
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public RolEnum Rol { get; set; }
        public EstadoEnum Estado { get; set; }
        public ICollection<Factura> Facturas { get; set; }
        = new List<Factura>();



    }
}
