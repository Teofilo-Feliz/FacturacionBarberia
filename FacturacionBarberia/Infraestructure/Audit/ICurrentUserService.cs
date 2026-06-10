namespace FacturacionBarberia.Infraestructure.Audit
{
    public interface ICurrentUserService
    {
        int? UsuarioId { get; }
    }
}
