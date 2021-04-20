using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Especialidad
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        [StringLength(150)]
        public string Nombre { get; set; }

        //RELACIONES
        public List<Enfermero> Enfermeros { get; set; }

    }
}