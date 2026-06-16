using FacturacionBarberia.Aplication.DTO;

namespace FacturacionBarberia.Aplication.ViewModel
{
    public class ConsultaViewModel<TFiltro, TResultado>
     where TFiltro : new()
    {
        public TFiltro Filtros { get; set; } = new();

        public List<TResultado> Resultados { get; set; } = new();
    }
}
