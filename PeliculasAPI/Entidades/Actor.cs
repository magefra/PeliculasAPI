﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Foto { get; set; }

        public List<PeliculaActores> PeliculaActores { get; set; }
    }
}
