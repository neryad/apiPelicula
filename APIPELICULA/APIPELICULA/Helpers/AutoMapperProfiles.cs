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
            CreateMap<ActorPacthDto, Actor>().ReverseMap();
            
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
            CreateMap<PeliculaCreacionDto, Pelicula>()
                .ForMember(c => c.Poster, 
                    options => options.Ignore());
            CreateMap<PeliculaPachtDto, Pelicula>().ReverseMap();
        }
    }
}