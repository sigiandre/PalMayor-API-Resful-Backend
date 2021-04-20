using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Tipo
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }
    }
}