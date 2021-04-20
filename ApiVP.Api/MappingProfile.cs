using AutoMapper;
using ApiVP.Domain.Entities;
using ApiVP.Domain.Models;

namespace ApiVP.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Especialidad, EspecialidadDTO>();
            CreateMap<EspecialidadCreateDTO, Especialidad>().ReverseMap();
            CreateMap<Oferta, OfertaDTO>();
            CreateMap<OfertaCreateDTO, Oferta>().ReverseMap();
            CreateMap<Enfermero, EnfermeroDTO>();
            CreateMap<EnfermeroCreateDTO, Enfermero>().ReverseMap();
            CreateMap<RangoHora, RangoHoraDTO>();
            CreateMap<RangoHoraCreateDTO, RangoHora>().ReverseMap();
            CreateMap<FechaAtencion, FechaAtencionDTO>();
            CreateMap<FechaAtencionCreateDTO, FechaAtencion>().ReverseMap();
            CreateMap<Oferta, OfertaDTO>();
            CreateMap<OfertaCreateDTO, Oferta>().ReverseMap();
            CreateMap<EnfermeroOferta, EnfermeroOfertaDTO>();
            CreateMap<EnfermeroOfertaCreateDTO, EnfermeroOferta>().ReverseMap();
            CreateMap<Servicio, ServicioDTO>();
            CreateMap<ServicioCreateDTO, Servicio>().ReverseMap();
            CreateMap<Rol, RolDTO>();
            CreateMap<RolCreateDTO, Rol>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioCreateDTO, Usuario>().ReverseMap();
            CreateMap<Persona, PersonaDTO>();
            CreateMap<PersonaCreateDTO, Persona>().ReverseMap();
            CreateMap<Familiar, FamiliarDTO>();
            CreateMap<FamiliarCreateDTO, Familiar>().ReverseMap();
            CreateMap<Anciano, AncianoDTO>();
            CreateMap<AncianoCreateDTO, Anciano>().ReverseMap();
            CreateMap<AncianoABVC, AncianoABVCDTO>();
            CreateMap<AncianoABVCCreateDTO, AncianoABVC>().ReverseMap();
            CreateMap<ABVC, ABVCDTO>();
            CreateMap<ABVCCreateDTO, ABVC>().ReverseMap();
            CreateMap<Tipo, TipoDTO>();
            CreateMap<TipoCreateDTO, Tipo>().ReverseMap();
            CreateMap<Grado, GradoDTO>();
            CreateMap<GradoCreateDTO, Grado>().ReverseMap();
        }
    }
}