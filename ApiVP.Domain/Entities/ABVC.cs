using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class ABVC
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Descripcion { get; set; }

        [Required]
        public int TipoId { get; set; }

        //Relaciones
        public Tipo Tipo { get; set; }
        public List<AncianoABVC> AncianoABVCs { get; set; }
    }
}