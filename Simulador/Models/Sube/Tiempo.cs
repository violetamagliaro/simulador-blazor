using System;

namespace TarjetaSube.Models.Sube
{
    public class Tiempo
    {
        public int Hora { get; private set; }
        public int Minutos { get; private set; }
        public int DiaSemana { get; private set; } // 1=Lunes ... 7=Domingo

        public Tiempo(int hora, int minutos, int diaSemana)
        {
            if (hora < 0 || hora > 23)
                throw new ArgumentException("Hora inválida. Debe estar entre 0 y 23.");
            if (minutos < 0 || minutos > 59)
                throw new ArgumentException("Minutos inválidos. Deben estar entre 0 y 59.");
            if (diaSemana < 1 || diaSemana > 7)
                throw new ArgumentException("Día de semana inválido. Debe estar entre 1 (Lunes) y 7 (Domingo).");

            Hora = hora;
            Minutos = minutos;
            DiaSemana = diaSemana;
        }

        public static Tiempo Ahora()
        {
            DateTime now = DateTime.Now;
            int dia = (int)now.DayOfWeek;
            // DayOfWeek: Sunday=0, Monday=1..Saturday=6 → convertimos a 1=Lunes..7=Domingo
            int diaSemana = dia == 0 ? 7 : dia;
            return new Tiempo(now.Hour, now.Minute, diaSemana);
        }

        public bool EsDiaHabil() => DiaSemana >= 1 && DiaSemana <= 5;

        public bool EsFinde() => DiaSemana == 6 || DiaSemana == 7;

        public bool EsHoraPico() =>
            (Hora >= 7 && Hora < 9) || (Hora >= 17 && Hora < 20);

        public bool EsDespuesDe(Tiempo otro)
        {
            if (Hora != otro.Hora) return Hora > otro.Hora;
            return Minutos > otro.Minutos;
        }

        public int DiferenciaEnMinutos(Tiempo otro)
        {
            return Math.Abs((Hora * 60 + Minutos) - (otro.Hora * 60 + otro.Minutos));
        }

        public bool EsMismoDia(Tiempo otro) => DiaSemana == otro.DiaSemana;

        public string NombreDia() => DiaSemana switch
        {
            1 => "Lunes",
            2 => "Martes",
            3 => "Miércoles",
            4 => "Jueves",
            5 => "Viernes",
            6 => "Sábado",
            7 => "Domingo",
            _ => "Desconocido"
        };

        public override string ToString() =>
            $"{Hora:D2}:{Minutos:D2} - {NombreDia()}";
    }
}
