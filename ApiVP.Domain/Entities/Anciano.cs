using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Anciano
    {
        public int Id { get; set; }

        [Required]
        public int FamiliarId { get; set; }

        [Required]
        public int PersonaId { get; set; }

        //Relaciones
        public Familiar Familiar { get; set; }
        public Persona Persona { get; set; }
        public List<AncianoABVC> AncianoABVCs { get; set; }
    }
}