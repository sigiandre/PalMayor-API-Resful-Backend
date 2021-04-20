using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVP.Domain.Models
{
    public class GradoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        //RELACIONES A MOSTRAR
        //public List<EnfermeroDTO> Enfermeros { get; set; }
    }
}