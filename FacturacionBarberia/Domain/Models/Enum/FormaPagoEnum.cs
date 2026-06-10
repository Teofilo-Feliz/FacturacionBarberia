using System.ComponentModel;

namespace FacturacionBarberia.Domain.Models.Enum
{
    public enum FormaPagoEnum
    {
        Seleccione = 0,
        [Description("Pago en efectivo")]
        Efectivo = 1,
        [Description("Tarjeta de Credito/Debito")]
        Tarjeta = 2,
        [Description("Transferencia Bancaria")]
        Transferencia = 3,

    }
}
