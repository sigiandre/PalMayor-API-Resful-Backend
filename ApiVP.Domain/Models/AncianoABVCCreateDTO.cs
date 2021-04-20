using System.ComponentModel.DataAnnotations;

namespace ApiVP.Domain.Models
{
    public class AncianoABVCCreateDTO
    {
        [Required]
        public int AncianoId { get; set; }

        [Required]
        public int ABVCId { get; set; }
    }
}