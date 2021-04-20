using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Entities
{
    public class EnfermeroOferta
    {
        [Required]
        public int EnfermeroId { get; set; }
        [Required]
        public int OfertaId { get; set; }

        //RELACIONES
        public Enfermero Enfermero { get; set; }
        public Oferta Oferta { get; set; }

    }
}