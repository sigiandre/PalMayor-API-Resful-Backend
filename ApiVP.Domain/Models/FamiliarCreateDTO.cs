using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class FamiliarCreateDTO
    {
        [Required]
        public int UsuarioId { get; set; }

        //RELACIones
        public PersonaCreateDTO Persona { get; set; }

    }
}