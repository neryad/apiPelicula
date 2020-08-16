using APIPELICULA.DTOS;
using APIPELICULA.Entidades;
using AutoMapper;

namespace APIPELICULA.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO,Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
        }
    }
}