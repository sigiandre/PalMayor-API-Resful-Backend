namespace ApiVP.Domain.Models
{
    public class ServicioDTO
    {
        public int Id { get; set; }
        public int EnfermeroId { get; set; }
        public int OfertaId { get; set; }
        public string Estado { get; set; }
        public decimal Costo { get; set; }

        //RELACIONES A MOSTRAR
        public EnfermeroDTO Enfermero { get; set; }
        public OfertaDTO Oferta { get; set; }
    }
}