using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs
{
    public class PeliculaDetalleDTO
    {
        public int Id { get; set; }


        public string Titulo { get; set; }

        public bool EnCines { get; set; }

        public DateTime FechaEstreno { get; set; }

        public string Poster { get; set; }

        public List<GeneroDTO> Generos { get; set; }


        public List<ActorPeliculaDetalleDTO> Actores { get; set; }


    }
}
