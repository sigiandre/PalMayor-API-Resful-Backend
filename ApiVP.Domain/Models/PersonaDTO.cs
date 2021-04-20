using System;
using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        public string DNI { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Foto { get; set; }
    }
}