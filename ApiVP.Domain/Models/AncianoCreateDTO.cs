using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ApiVP.Domain.Models
{
    public class AncianoCreateDTO
    {
        [Required]
        public int FamiliarId { get; set; }

        //para llenar
        public PersonaCreateDTO Persona { get; set; }
        public List<AncianoABVCCreateDTO> AncianoABVCs { get; set; }
    }
}