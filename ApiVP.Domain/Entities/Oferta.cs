using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Oferta
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Estado { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Direccion { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Descripcion { get; set; }

        [Required]
        public int AncianoId { get; set; }

        //RELACIONES
        public Anciano Anciano { get; set; }
        public List<EnfermeroOferta> EnfermeroOfertas { get; set; }
        public List<FechaAtencion> FechaAtenciones { get; set; }

    }
}