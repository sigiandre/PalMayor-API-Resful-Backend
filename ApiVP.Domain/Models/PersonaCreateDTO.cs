using System;
using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class PersonaCreateDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Ingrese nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Ingrese apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "DNI incorrecto", MinimumLength = 8)]
        public string DNI { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Solo F o M")]
        public string Sexo { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public string Foto { get; set; }

    }
}