using APIPELICULA.DTOS;
using APIPELICULA.Entidades;
using AutoMapper;

namespace APIPELICULA.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDto>().ReverseMap();
            CreateMap<GeneroCreacionDto,Genero>();

            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<ActorCreacionDto, Actor>()
                .ForMember(c => c.Foto, 
                    options => options.Ignore());
        }
    }
}