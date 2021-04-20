namespace ApiVP.Domain.Models
{
    public class EnfermeroOfertaDTO
    {
        public int EnfermeroId { get; set; }
        public int OfertaId { get; set; }

        //RELACIONES A MOSTRAR
        public EnfermeroDTO Enfermero { get; set; }
        public OfertaDTO Oferta { get; set; }
    }
}