using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Models
{
    public class EnfermeroCreateDTO
    {
        [Required]
        [StringLength(8, ErrorMessage = "La colegiatura consta de {1} caracteres", MinimumLength = 6)]
        public string Colegiatura { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres")]
        public string Universidad { get; set; }

        public string Experiencia { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int EspecialidadId { get; set; }

        [Required]
        public int GradoId { get; set; }

        //RELACIONES A LLENAR
        public PersonaCreateDTO Persona { get; set; }
    }
}