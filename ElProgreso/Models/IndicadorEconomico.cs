using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElProgreso.Models
{
    public class IndicadorEconomico
    {
        public IndicadorEconomico(string codigo, DateTime fecha, double valor)
        {
            Codigo = codigo;
            Fecha = fecha;
            Valor = valor;
        }

        public string Codigo { get; }
        public DateTime Fecha { get; }
        public double Valor { get; }
    }
}