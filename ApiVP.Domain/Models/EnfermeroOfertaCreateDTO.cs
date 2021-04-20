using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class EnfermeroOfertaCreateDTO
    {
        [Required]
        public int EnfermeroId { get; set; }

        [Required]
        public int OfertaId { get; set; }
    }
}