using System;

namespace TarjetaSube.Models.Sube
{
    // Media tarifa: 50% del precio normal. Para estudiantes secundarios.
    public class MedioBoleto : Tarjeta
    {
        private static readonly decimal FACTOR_MEDIO = 0.5m;

        // Puede no existir hasta que haya un viaje
        private Tiempo? tiempoUltimoViaje;

        private decimal _ultimoPago;

        public MedioBoleto(decimal saldoInicial) : base(saldoInicial)
        {
            tiempoUltimoViaje = null;
        }

        protected decimal ObtenerTarifaMedio()
        {
            // 50% de la tarifa normal base
            return TARIFA_NORMAL * FACTOR_MEDIO;
        }

        public override bool TieneSaldoSuficiente()
        {
            decimal tarifa = ObtenerTarifaMedio();
            return saldo >= tarifa || saldo >= -(tarifa / 2);
        }

        public override void DebitarSaldo()
        {
            DebitarSaldo(ObtenerTarifaMedio());
        }

        public override void DebitarSaldo(decimal monto)
        {
            if (!TieneSaldoSuficiente())
                throw new InvalidOperationException("Saldo insuficiente.");

            _ultimoPago = ObtenerTarifaMedio();

            saldo -= _ultimoPago;
            tiempoUltimoViaje = Tiempo.Ahora();
            viajesEsteMes++;
        }

        public override decimal ObtenerUltimoPago()
        {
            return _ultimoPago;
        }

        public Tiempo? ObtenerTiempoUltimoViaje()
        {
            return tiempoUltimoViaje;
        }
    }
}