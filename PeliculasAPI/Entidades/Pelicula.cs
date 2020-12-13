using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class Pelicula : IId
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Titulo { get; set; }

        public bool EnCines { get; set; }

        public DateTime FechaEstreno { get; set; }

        public string Poster { get; set; }

        public List<PeliculaActores> PeliculaActores { get; set; }

        public List<PeliculasGeneros> PeliculasGeneros { get; set; }
    }
}
