using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class AncianoABVC
    {
        [Required]
        public int AncianoId { get; set; }

        [Required]
        public int ABVCId { get; set; }

        //Relaciones
        public Anciano Anciano { get; set; }
        public ABVC ABVC { get; set; }

    }

}