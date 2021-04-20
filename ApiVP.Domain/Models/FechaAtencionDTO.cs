using System;
using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class FechaAtencionDTO
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public int RangoHoraId { get; set; }
        public int OfertaId { get; set; }

        //RELACIONES A MOSTRAR
        public RangoHoraDTO RangoHora { get; set; }
    }
}