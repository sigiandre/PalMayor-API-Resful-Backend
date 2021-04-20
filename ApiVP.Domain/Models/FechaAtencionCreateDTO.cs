using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Models
{
    public class FechaAtencionCreateDTO
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int RangoHoraId { get; set; }
    }
}