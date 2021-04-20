using System.Collections.Generic;

namespace ApiVP.Domain.Models
{
    public class AncianoDTO
    {
        public int Id { get; set; }
        public int FamiliarId { get; set; }
        public int PersonaId { get; set; }

        //Relacines
        public FamiliarDTO Familiar { get; set; }
        public PersonaDTO Persona { get; set; }
        public List<AncianoABVCDTO> AncianoABVCs { get; set; }
    }
}