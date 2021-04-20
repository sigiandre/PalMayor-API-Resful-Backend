using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class RangoHoraDTO
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{hh:mm tt}", ApplyFormatInEditMode = true)]
        public TimeSpan Inicio { get; set; }

        [DisplayFormat(DataFormatString = "{hh:mm tt}", ApplyFormatInEditMode = true)]
        public TimeSpan Fin { get; set; }

        //RELACIONES A MOSTRAR
        public List<FechaAtencionDTO> FechaAtenciones { get; set; }
    }
}