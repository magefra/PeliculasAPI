using AutoMapper;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System.Collections.Generic;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<ActorPathDTO, Actor>().ReverseMap();


            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.PeliculaActores, options => options.MapFrom(MapPeliculasActores));
            CreateMap<PeliculaPathDTO, Pelicula>().ReverseMap();


            CreateMap<Pelicula, PeliculaDetalleDTO>()
                 .ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros))
                 .ForMember(x => x.Actores, options => options.MapFrom(MapPeliculasActores));




        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pelicula"></param>
        /// <param name="peliculaDetalleDTO"></param>
        /// <returns></returns>
        private List<ActorPeliculaDetalleDTO> MapPeliculasActores(Pelicula pelicula, PeliculaDetalleDTO peliculaDetalleDTO)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();

            if(resultado == null)
            {
                return resultado;
            }


            foreach (var actorPelicula in pelicula.PeliculaActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO()
                {
                    ActorId = actorPelicula.ActorId,
                    Personaje = actorPelicula.Personaje,
                    NombrePersona = actorPelicula.Actor.Nombre

                });
            }

            return resultado;
        }


        private List<GeneroDTO> MapPeliculasGeneros(Pelicula pelicula, PeliculaDetalleDTO peliculaDetalleDTO)
        {
            var resultado = new List<GeneroDTO>();


            if(pelicula.PeliculasGeneros == null)
            {
                return resultado;
            }


            foreach (var generoPelicula in pelicula.PeliculasGeneros)
            {
                resultado.Add(new GeneroDTO()
                {
                    Id = generoPelicula.GeneroId,
                    Nombre = generoPelicula.Genero.Nombre
                });


                
            }

            return resultado;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="peliculaCreacionDTO"></param>
        /// <param name="pelicula"></param>
        /// <returns></returns>
        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, 
                                                           Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();

            if(peliculaCreacionDTO.GenerosIDs == null)
            {
                return resultado;
            }


            foreach(var id in peliculaCreacionDTO.GenerosIDs)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id});
            }

            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peliculaCreacionDTO"></param>
        /// <param name="pelicula"></param>
        /// <returns></returns>
        private List<PeliculaActores> MapPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO,
                                                           Pelicula pelicula)
        {
            var resultado = new List<PeliculaActores>();

            if (peliculaCreacionDTO.Actores == null)
            {
                return resultado;
            }


            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculaActores() { ActorId = actor.ActorId, Personaje = actor.Personaje });
            }

            return resultado;
        }
    }
}
