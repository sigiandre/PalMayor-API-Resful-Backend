namespace ApiVP.Domain.Models
{
    public class AncianoABVCDTO
    {
        public int AncianoId { get; set; }
        public int ABVCId { get; set; }

        ///Relaciones
        public AncianoDTO Anciano { get; set; }
        public ABVCDTO ABVC { get; set; }
    }

}