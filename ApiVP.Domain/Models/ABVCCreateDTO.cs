using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class ABVCCreateDTO
    {
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required]
        public int TipoId { get; set; }
    }
}