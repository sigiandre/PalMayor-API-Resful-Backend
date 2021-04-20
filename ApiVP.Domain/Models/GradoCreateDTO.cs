using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Models
{
    public class GradoCreateDTO
    {
        [Required]
        [StringLength(20, ErrorMessage = "Máximo {1} caracteres")]
        public string Nombre { get; set; }
    }
}