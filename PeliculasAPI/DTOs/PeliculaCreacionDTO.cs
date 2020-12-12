﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Enum;
using PeliculasAPI.Helpers;
using PeliculasAPI.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class PeliculaCreacionDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Titulo { get; set; }

        public bool EnCines { get; set; }

        public DateTime FechaEstreno { get; set; }


        [PesoArchivoValidacion(pesoMaximoMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType  = typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIDs { get; set; }


        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculasCreacionDTO>>))]
        public List<ActorPeliculasCreacionDTO> Actores { get; set; }
    }
}
