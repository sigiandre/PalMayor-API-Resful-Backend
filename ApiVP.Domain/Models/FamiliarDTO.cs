using System.Collections.Generic;

namespace ApiVP.Domain.Models
{
    public class FamiliarDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PersonaId { get; set; }

        //Relaciones
        public PersonaDTO Persona { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public List<AncianoDTO> Ancianos { get; set; }
    }
}