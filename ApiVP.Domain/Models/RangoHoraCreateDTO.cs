using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class RangoHoraCreateDTO
    {
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Inicio { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Fin { get; set; }
    }
}