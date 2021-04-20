using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Familiar
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int PersonaId { get; set; }

        //Relaciones     

        public Usuario Usuario { get; set; }
        public Persona Persona { get; set; }
        public List<Anciano> Ancianos { get; set; }
    }
}