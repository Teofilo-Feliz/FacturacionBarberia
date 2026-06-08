namespace FacturacionBarberia.Domain.Models.Entities
{
    public abstract class AuditoriaEntities
    {
        public DateTime FechaCreacion { get; set; }

        public int? UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int? UsuarioModificacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public int? UsuarioEliminacion { get; set; }

        public bool EstaEliminado { get; set; }
    }
}
