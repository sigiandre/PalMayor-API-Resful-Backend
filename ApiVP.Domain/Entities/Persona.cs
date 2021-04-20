using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Persona
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Apellidos { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(8)]
        public string DNI { get; set; }

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string Telefono { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Foto { get; set; }
    }
}