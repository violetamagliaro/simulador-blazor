using System;

namespace TarjetaSube.Models.Sube
{
    public class Boleto
    {
        public DateTime Fecha { get; private set; }
        public string TipoTarjeta { get; private set; }
        public string LineaColectivo { get; private set; }
        public decimal TotalAbonado { get; private set; }
        public decimal SaldoRestante { get; private set; }
        public string IdTarjeta { get; private set; }
        public bool EsViajeGratuito { get; private set; }
        public bool TieneSaldoNegativo { get; private set; }

        public Boleto(string tipoTarjeta, string lineaColectivo, decimal totalAbonado,
                      decimal saldoRestante, string idTarjeta, bool esViajeGratuito = false)
        {
            Fecha = DateTime.Now;
            TipoTarjeta = tipoTarjeta;
            LineaColectivo = lineaColectivo;
            TotalAbonado = totalAbonado;
            SaldoRestante = saldoRestante;
            IdTarjeta = idTarjeta;
            EsViajeGratuito = esViajeGratuito;
            TieneSaldoNegativo = saldoRestante < 0;
        }

        public void MostrarBoleto()
        {
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("         🚌 BOLETO         ");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine($"Fecha:        {Fecha:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Línea:        {LineaColectivo}");
            Console.WriteLine($"Tipo tarjeta: {TipoTarjeta}");
            Console.WriteLine($"ID tarjeta:   {IdTarjeta}");
            Console.WriteLine($"Abonado:      ${TotalAbonado:F2}");

            if (EsViajeGratuito)
                Console.WriteLine("              ✓ Viaje gratuito (franquicia)");

            Console.WriteLine($"Saldo:        ${SaldoRestante:F2}");

            if (TieneSaldoNegativo)
                Console.WriteLine("              ⚠ Saldo negativo. Recargue en la próxima oportunidad.");

            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━");
        }
    }
}
