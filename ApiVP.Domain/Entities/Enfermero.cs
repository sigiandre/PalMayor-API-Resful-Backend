using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Enfermero
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(8)")]
        public string Colegiatura { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Universidad { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Experiencia { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int PersonaId { get; set; }

        [Required]
        public int EspecialidadId { get; set; }

        [Required]
        public int GradoId { get; set; }

        //RELACIONES
        public Usuario Usuario { get; set; }
        public Persona Persona { get; set; }
        public Especialidad Especialidad { get; set; }
        public Grado Grado { get; set; }
        public List<EnfermeroOferta> EnfermeroOfertas { get; set; }
        public List<Servicio> Servicios { get; set; }
    }
}