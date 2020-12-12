using Microsoft.AspNetCore.Http;
using PeliculasAPI.Enum;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PeliculasAPI.Validaciones
{
    public class TipoArchivoValidacion : ValidationAttribute
    { 
        /// <summary>
        /// 
        /// </summary>
        private readonly string[] _tiposValidos;

        /// <summary>
        /// 
        /// </summary>
        private readonly GrupoTipoArchivo _grupoTipoArchivo;



        public TipoArchivoValidacion(string[] tiposValidos)
        {
            _tiposValidos = tiposValidos;
        }

        public TipoArchivoValidacion(GrupoTipoArchivo grupoTipoArchivo)
        {
            if(grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                _tiposValidos = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }


            if (!_tiposValidos.Contains(formFile.ContentType))
            {

                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes: " +
                    $"{string.Join(", ",_tiposValidos)}");
            }

            return ValidationResult.Success;

        }


    }
}
