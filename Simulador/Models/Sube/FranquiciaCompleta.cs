using System;

namespace TarjetaSube.Models.Sube
{
    // Franquicia completa: 2 viajes gratuitos por día. Para jubilados/pensionados.
    public class FranquiciaCompleta : Tarjeta
    {
        private const int MAX_VIAJES_GRATUITOS_DIA = 2;
        private int viajesGratuitosHoy;
        private int diaActual;

        public FranquiciaCompleta(decimal saldoInicial) : base(saldoInicial)
        {
            viajesGratuitosHoy = 0;
            diaActual = DateTime.Now.Day;
        }

        public override bool TieneSaldoSuficiente()
        {
            VerificarDia();
            // Si todavía tiene viajes gratuitos disponibles, siempre puede viajar
            if (viajesGratuitosHoy < MAX_VIAJES_GRATUITOS_DIA)
                return true;
            // Si agotó los gratuitos, necesita saldo como tarjeta normal
            return base.TieneSaldoSuficiente();
        }

        public override void DebitarSaldo()
        {
            VerificarDia();

            if (viajesGratuitosHoy < MAX_VIAJES_GRATUITOS_DIA)
            {
                // Viaje gratuito: no descuenta saldo
                ultimoPago = 0m;
                viajesGratuitosHoy++;
                viajesEsteMes++;
            }
            else
            {
                // Agotó los gratuitos: paga tarifa normal
                base.DebitarSaldo();
            }
        }

        public override void DebitarSaldo(decimal monto)
        {
            VerificarDia();

            if (viajesGratuitosHoy < MAX_VIAJES_GRATUITOS_DIA)
            {
                ultimoPago = 0m;
                viajesGratuitosHoy++;
                viajesEsteMes++;
            }
            else
            {
                base.DebitarSaldo(monto);
            }
        }

        public int ViajesGratuitosRestantesHoy()
        {
            VerificarDia();
            return Math.Max(0, MAX_VIAJES_GRATUITOS_DIA - viajesGratuitosHoy);
        }

        private void VerificarDia()
        {
            int diaReal = DateTime.Now.Day;
            if (diaActual != diaReal)
            {
                viajesGratuitosHoy = 0;
                diaActual = diaReal;
            }
        }
    }
}
