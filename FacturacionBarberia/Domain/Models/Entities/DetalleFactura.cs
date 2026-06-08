namespace FacturacionBarberia.Domain.Models.Entities
{
    public class DetalleFactura
    {
        public int DetalleFacturaId { get; set; }

        public int FacturaId { get; set; }

        public int ServicioId { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public decimal SubTotal => Precio * Cantidad;

        public Factura Factura { get; set; } = null!;

        public Servicio Servicio { get; set; } = null!;
    }
}
