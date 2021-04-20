using System;
using System.Collections.Generic;

namespace ApiVP.Domain.Models
{
    public class EnfermeroDTO
    {
        public int Id { get; set; }
        public string Colegiatura { get; set; }
        public string Universidad { get; set; }
        public string Experiencia { get; set; }
        public int PersonaId { get; set; }
        public int EspecialidadId { get; set; }
        public int GradoId { get; set; }

        //RELACIONES A MOSTRAR
        public PersonaDTO Persona { get; set; }
        public EspecialidadDTO Especialidad { get; set; }
        public GradoDTO Grado { get; set; }
        public List<EnfermeroOfertaDTO> EnfermeroOfertas { get; set; }

        public List<ServicioDTO> Servicios { get; set; }
    }
}