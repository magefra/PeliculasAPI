using Microsoft.AspNetCore.Http;
using PeliculasAPI.Enum;
using PeliculasAPI.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class ActorPathDTO
    {
        [Required]
        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }


        [PesoArchivoValidacion(pesoMaximoMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
