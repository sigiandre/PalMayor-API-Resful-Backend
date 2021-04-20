using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class TipoCreateDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }
    }
}