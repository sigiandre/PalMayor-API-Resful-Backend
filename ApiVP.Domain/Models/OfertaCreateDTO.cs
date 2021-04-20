using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Models
{
    public class OfertaCreateDTO
    {
        [Required]
        public string Direccion { get; set; }

        public string Descripcion { get; set; }

        [Required]
        public int AncianoId { get; set; }

        //RELACIONES A CREAR
        public List<FechaAtencionCreateDTO> FechaAtenciones { get; set; }
    }
}