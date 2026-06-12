using FacturacionBarberia.Aplication.DTO;

namespace FacturacionBarberia.Aplication.ViewModel
{
    public class UsuarioConsultaViewModel
    {
        public ObtenerUsuarioRequest Filtros { get; set; } = new();

        public List<ObtenerUsuarioResponse> Usuarios { get; set; } = new();
    }
}
