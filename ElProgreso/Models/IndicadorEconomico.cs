using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElProgreso.Models
{
    public class IndicadorEconomico
    {
        public IndicadorEconomico()
        {
        }

        public IndicadorEconomico(string codigo, DateTime fecha, double valor)
        {
            Codigo = codigo;
            Fecha = fecha;
            Valor = valor;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public double Valor { get; set; }
    }
}