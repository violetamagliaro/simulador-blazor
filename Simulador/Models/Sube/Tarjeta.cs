using System;

namespace TarjetaSube.Models.Sube
{
    public class Tarjeta
    {
        protected decimal saldo;
        protected static readonly decimal TARIFA_NORMAL = 1200m;
        protected static readonly decimal TARIFA_DESC_20 = TARIFA_NORMAL * 0.80m;  // 30+ viajes/mes
        protected static readonly decimal TARIFA_DESC_25 = TARIFA_NORMAL * 0.75m;  // 80 viajes/mes exacto
        protected static readonly decimal LIMITE_SALDO_CARGA = 36000m;
        protected static readonly decimal LIMITE_NEGATIVO = -(TARIFA_NORMAL / 2);  // -600

        public decimal ultimoPago;
        private string idTarjeta;
        protected int viajesEsteMes;
        private int mesActual;

        public Tarjeta(decimal saldoInicial)
        {
            if (!EsMontoValido(saldoInicial))
                throw new ArgumentException($"Saldo inicial no válido. Montos aceptados: 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000.");

            saldo = saldoInicial;
            idTarjeta = GenerarIDNumerico(16);
            viajesEsteMes = 0;
            mesActual = DateTime.Now.Month;
        }

        private bool EsMontoValido(decimal monto)
        {
            decimal[] montosAceptados = { 2000m, 3000m, 4000m, 5000m, 6000m, 7000m, 8000m, 9000m };
            return Array.Exists(montosAceptados, m => m == monto);
        }

        public virtual bool TieneSaldoSuficiente()
        {
            return saldo >= ObtenerTarifa() || saldo >= LIMITE_NEGATIVO;
        }

        protected decimal ObtenerTarifa()
        {
            VerificarMes();
            if (viajesEsteMes < 30)
                return TARIFA_NORMAL;
            else if (viajesEsteMes < 80)
                return TARIFA_DESC_20;
            else if (viajesEsteMes == 80)
                return TARIFA_DESC_25;
            else
                return TARIFA_NORMAL;  // Más de 80: vuelve a tarifa normal
        }

        // Versión sin parámetro: debita la tarifa calculada automáticamente
        public virtual void DebitarSaldo()
        {
            decimal monto = ObtenerTarifa();
            DebitarSaldo(monto);
        }

        public virtual void DebitarSaldo(decimal monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto a debitar debe ser positivo.");

            if (!TieneSaldoSuficiente())
                throw new InvalidOperationException("Saldo insuficiente.");

            ultimoPago = monto;
            saldo -= monto;
            viajesEsteMes++;
            VerificarMes();
        }

        public decimal ObtenerSaldo() => saldo;
        public string ObtenerID() => idTarjeta;
        public virtual decimal ObtenerUltimoPago() => ultimoPago;
        public int ObtenerViajesEsteMes() => viajesEsteMes;

        public void RecargarSaldo(decimal monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto de recarga debe ser positivo.");

            // Primero cancela la deuda si hay saldo negativo
            if (saldo < 0)
            {
                decimal deuda = Math.Abs(saldo);
                if (monto >= deuda)
                {
                    monto -= deuda;
                    saldo = 0;
                }
                else
                {
                    saldo += monto;
                    monto = 0;
                }
            }

            // Carga el saldo sin superar el límite
            if (saldo + monto > LIMITE_SALDO_CARGA)
            {
                decimal cargaPosible = LIMITE_SALDO_CARGA - saldo;
                saldo += cargaPosible;
                decimal excedente = monto - cargaPosible;
                Console.WriteLine($"Carga parcial: se acreditaron ${cargaPosible:F2}. Excedente pendiente: ${excedente:F2}");
            }
            else
            {
                saldo += monto;
                Console.WriteLine($"Recarga exitosa. Se acreditaron ${monto:F2}.");
            }
        }

        public bool TieneSaldoNegativo() => saldo < 0;

        private void VerificarMes()
        {
            int mesActualReal = DateTime.Now.Month;
            if (mesActual != mesActualReal)
            {
                viajesEsteMes = 0;
                mesActual = mesActualReal;
            }
        }

        private string GenerarIDNumerico(int longitud)
        {
            Random random = new Random();
            string id = "";
            for (int i = 0; i < longitud; i++)
                id += random.Next(0, 10).ToString();
            return id;
        }
    }
}
