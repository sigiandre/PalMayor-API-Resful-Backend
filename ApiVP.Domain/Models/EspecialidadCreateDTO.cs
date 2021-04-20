using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Models
{
    public class EspecialidadCreateDTO
    {

        [Required]
        [StringLength(150, ErrorMessage = "Máximo {1} caracteres")]
        public string Nombre { get; set; }
    }
}