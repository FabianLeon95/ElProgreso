using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElProgreso.Models
{
    public class Pregunta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "El nombre debe contener almenos 3 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "El titulo debe contener almenos 10 caracteres.")]
        public string Titulo { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "La descripcion debe contener almenos 10 caracteres.")]
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}