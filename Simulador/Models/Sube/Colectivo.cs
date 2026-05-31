using System;

namespace TarjetaSube.Models.Sube
{
    public class Colectivo
    {
        private string linea;
        private const decimal TARIFA_INTERURBANA = 2500m;

        public Colectivo(string linea = "102")
        {
            this.linea = linea;
        }

        public Boleto PagarCon(Tarjeta tarjeta, Tiempo tiempoActual, bool esInterurbano = false)
        {
            if (tarjeta == null)
                throw new ArgumentNullException(nameof(tarjeta));

            if (!tarjeta.TieneSaldoSuficiente())
                throw new InvalidOperationException("Saldo insuficiente en la tarjeta.");

            bool esGratuito = false;

            if (esInterurbano)
            {
                tarjeta.DebitarSaldo(TARIFA_INTERURBANA);
            }
            else
            {
                // Si es FranquiciaCompleta y el próximo viaje es gratuito, lo detectamos
                if (tarjeta is FranquiciaCompleta franquicia &&
                    franquicia.ViajesGratuitosRestantesHoy() > 0)
                {
                    esGratuito = true;
                }
                tarjeta.DebitarSaldo();
            }

            return new Boleto(
                tipoTarjeta: ObtenerNombreTipoTarjeta(tarjeta),
                lineaColectivo: linea,
                totalAbonado: tarjeta.ObtenerUltimoPago(),
                saldoRestante: tarjeta.ObtenerSaldo(),
                idTarjeta: tarjeta.ObtenerID(),
                esViajeGratuito: esGratuito
            );
        }

        private string ObtenerNombreTipoTarjeta(Tarjeta t) => t switch
        {
            FranquiciaCompleta => "Franquicia Completa",
            MedioBoleto => "Medio Boleto",
            _ => "Tarjeta Normal"
        };
    }
}
