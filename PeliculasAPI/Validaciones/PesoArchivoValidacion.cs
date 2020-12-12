using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validaciones
{
    public class PesoArchivoValidacion : ValidationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly int _pesoMaximoMegaBytes;



        public PesoArchivoValidacion(int pesoMaximoMegaBytes)
        {
            _pesoMaximoMegaBytes = pesoMaximoMegaBytes;
        }


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


            if(formFile.Length > _pesoMaximoMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {_pesoMaximoMegaBytes}mb");
            }


            return ValidationResult.Success;


        }
    }
}
