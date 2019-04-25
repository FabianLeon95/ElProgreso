using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ElProgreso.Models
{
    public class Respuesta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PreguntaId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "El nombre debe contener almenos 3 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "La respuesta debe contener almenos 10 caracteres.")]
        public string Contenido { get; set; }

        public DateTime Fecha { get; set; }

        public virtual Pregunta Pregunta { get; set; }
    }
}