using System.Collections.Generic;

namespace TarjetaSube.Models.Tienda
{
    public class Tienda
    {
        public List<Producto> Productos { get; private set; }

        public decimal DineroEnCaja { get; private set; }

        public Tienda()
        {
            Productos = new List<Producto>
            {
                new Producto("Chocolate", 1000, 15),
                new Producto("Café", 800, 20),
                new Producto("Galletitas", 500, 25),
                new Producto("Gaseosa", 1200, 10),
                new Producto("Pizza", 2500, 8),
                new Producto("Manzana", 300, 30),
                new Producto("Arroz", 600, 18),
                new Producto("Leche", 900, 22),
                new Producto("Yogur", 700, 12),
                new Producto("Fideos", 550, 20),
                new Producto("Manteca", 1100, 9),
                new Producto("Jugo", 850, 14)
            };
        }

        public void AgregarDinero(decimal monto)
        {
            DineroEnCaja += monto;
        }
    }
}