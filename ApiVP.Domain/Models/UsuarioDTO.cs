namespace ApiVP.Domain.Models
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public int RolId { get; set; }

        //Relaciones         
        public RolDTO Rol { get; set; }
    }
}