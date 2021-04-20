using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Correo { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Contrasenya { get; set; }
        //Relaciones
        public int RolId { get; set; }
        public Rol Rol { get; set; }



    }

}