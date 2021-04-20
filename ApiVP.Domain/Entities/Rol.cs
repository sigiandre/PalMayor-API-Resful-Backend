using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Rol
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string Nombre { get; set; }
    }
}