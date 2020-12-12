using System;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class PeliculaPathDTO
    {
        [Required]
        [StringLength(30)]
        public string Titulo { get; set; }

        public bool EnCines { get; set; }

        public DateTime FechaEstreno { get; set; }

    }
}
