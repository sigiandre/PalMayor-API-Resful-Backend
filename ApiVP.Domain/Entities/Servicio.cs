using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Servicio
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Estado { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        [Required]
        public int EnfermeroId { get; set; }

        [Required]
        public int OfertaId { get; set; }

        //RELACIONES
        public Enfermero Enfermero { get; set; }
        public Oferta Oferta { get; set; }
    }
}