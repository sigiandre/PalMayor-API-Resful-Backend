using System;
using System.Collections.Generic;

namespace ApiVP.Domain.Models
{
    public class OfertaDTO
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public int AncianoId { get; set; }

        //
        public AncianoDTO Anciano { get; set; }
        public List<FechaAtencionDTO> FechaAtenciones { get; set; }


    }
}