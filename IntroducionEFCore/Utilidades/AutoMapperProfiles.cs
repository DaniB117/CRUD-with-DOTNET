using AutoMapper;
using IntroducionEFCore.DTOs;
using IntroducionEFCore.Entidades;

namespace IntroducionEFCore.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<ActorCreacionDTO, Actor>();

            CreateMap<ComentarioCreacionDTO, Comentario>();

            CreateMap<PeliculasCreacionDTO, Pelicula>()
                .ForMember(ent => ent.Generos,dto => dto.MapFrom
                (campo => campo.Generos.Select(id => new Genero { Id = id})));

            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>();

            CreateMap<Actor, ActorDTO>();
        }

        
    }
}
