﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class Genero
    {

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }


        public List<PeliculaActores> PeliculaActores { get; set; }
    }
}
