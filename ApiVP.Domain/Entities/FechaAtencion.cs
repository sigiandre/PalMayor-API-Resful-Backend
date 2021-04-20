using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiVP.Domain.Entities
{
    public class FechaAtencion
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Required]
        public int RangoHoraId { get; set; }

        [Required]
        public int OfertaId { get; set; }

        //RELACIONES
        public Oferta Oferta { get; set; }
        public RangoHora RangoHora { get; set; }


    }
}