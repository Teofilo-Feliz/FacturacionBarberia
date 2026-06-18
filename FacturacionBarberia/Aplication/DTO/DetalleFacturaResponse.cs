namespace FacturacionBarberia.Aplication.DTO
{
    public class DetalleFacturaResponse
    {
        public string Servicio { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
    }
}
