namespace TarjetaSube.Models.Tienda
{
    public class Producto
    {
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }

        public Producto(string nombre, decimal costo, int stock)
        {
            Nombre = nombre;
            Costo = costo;
            PrecioVenta = costo * 1.30m;
            Stock = stock;
        }
    }
}