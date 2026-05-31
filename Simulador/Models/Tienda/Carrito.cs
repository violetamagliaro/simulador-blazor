using System.Collections.Generic;
using System.Linq;

namespace TarjetaSube.Models.Tienda
{
    public class Carrito
    {
        public List<ItemCarrito> Items { get; private set; } = new();

        public int CantidadProductos => Items.Sum(i => i.Cantidad);

        public decimal Total => Items.Sum(i => i.Producto.PrecioVenta * i.Cantidad);

        public void AgregarProducto(Producto producto)
        {
            var existente = Items.FirstOrDefault(i => i.Producto.Nombre == producto.Nombre);

            if (existente != null)
            {
                existente.Cantidad++;
            }
            else
            {
                Items.Add(new ItemCarrito
                {
                    Producto = producto,
                    Cantidad = 1
                });
            }

            producto.Stock--;
        }

        public void Vaciar()
        {
            Items.Clear();
        }
    }

    public class ItemCarrito
    {
        public required Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}