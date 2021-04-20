namespace ApiVP.Domain.Models
{
    public class ABVCDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int TipoId { get; set; }

        //Relaciones
        public TipoDTO Tipo { get; set; }

    }
}