using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Models
{
    public class UsuarioCreateDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Correo { get; set; }


        [Required]
        [StringLength(16, ErrorMessage = "Mínimo {2} caracteres y máximo {1}", MinimumLength = 8)]
        public string Contrasenya { get; set; }
        //
        public int RolId { get; set; }

    }
}