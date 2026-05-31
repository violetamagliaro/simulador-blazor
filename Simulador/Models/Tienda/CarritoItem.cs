namespace TarjetaSube.Models.Tienda
{
    public class CarritoItem
    {
        public Producto Producto { get; set; } = null!;
        public int Cantidad { get; set; }

        public decimal Subtotal =>
            Producto.PrecioVenta * Cantidad;
    }
}