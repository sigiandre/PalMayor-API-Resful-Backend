using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class RolCreateDTO
    {
        [Required]
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string Nombre { get; set; }
    }
}