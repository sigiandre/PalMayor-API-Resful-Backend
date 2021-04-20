using Microsoft.EntityFrameworkCore;
using ApiVP.Domain.Entities;

namespace ApiVP.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnfermeroOferta>().HasKey(x => new { x.EnfermeroId, x.OfertaId });
            modelBuilder.Entity<EnfermeroOferta>().HasOne(x => x.Enfermero).WithMany(x => x.EnfermeroOfertas).HasForeignKey(x => x.EnfermeroId);
            modelBuilder.Entity<EnfermeroOferta>().HasOne(x => x.Oferta).WithMany(x => x.EnfermeroOfertas).HasForeignKey(x => x.OfertaId);
            modelBuilder.Entity<AncianoABVC>().HasKey(x => new { x.AncianoId, x.ABVCId });
            modelBuilder.Entity<AncianoABVC>().HasOne(x => x.Anciano).WithMany(x => x.AncianoABVCs).HasForeignKey(x => x.AncianoId);
            modelBuilder.Entity<AncianoABVC>().HasOne(x => x.ABVC).WithMany(x => x.AncianoABVCs).HasForeignKey(x => x.ABVCId);
        }

        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Enfermero> Enfermeros { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<EnfermeroOferta> EnfermeroOfertas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<FechaAtencion> FechaAtenciones { get; set; }
        public DbSet<RangoHora> RangoHoras { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Familiar> Familiares { get; set; }
        public DbSet<Anciano> Ancianos { get; set; }
        public DbSet<ABVC> ABVCs { get; set; }
        public DbSet<AncianoABVC> AncianoABVCs { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
    }
}