using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class RangoHora
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "time(0)")]
        public TimeSpan Inicio { get; set; }

        [Required]
        [Column(TypeName = "time(0)")]
        public TimeSpan Fin { get; set; }

        //RELACIONES
        public List<FechaAtencion> FechaAtenciones { get; set; }
    }
}