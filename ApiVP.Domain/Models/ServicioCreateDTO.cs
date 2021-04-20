using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Models
{
    public class ServicioCreateDTO
    {
        [Required]
        public int EnfermeroId { get; set; }

        [Required]
        public int OfertaId { get; set; }

    }
}