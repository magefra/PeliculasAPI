using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PeliculasAPI.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// 
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;



        public AlmacenadorArchivosLocal(IWebHostEnvironment env,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="contenedor"></param>
        /// <returns></returns>
        public  Task BorrarArchivo(string ruta, string contenedor)
        {
            if(ruta == null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(_env.WebRootPath, contenedor, nombreArchivo);

                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
                
            }

            return Task.FromResult(0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contenido"></param>
        /// <param name="extension"></param>
        /// <param name="contenedor"></param>
        /// <param name="ruta"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contenido"></param>
        /// <param name="extension"></param>
        /// <param name="contenedor"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";

            string folder = Path.Combine(_env.WebRootPath, contenedor);


            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }


            string ruta = Path.Combine(folder, nombreArchivo);

            await File.WriteAllBytesAsync(ruta, contenido);


            var urlActual = $"{_httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{_httpContextAccessor.HttpContext.Request.Host}";


            var urlParaBd = Path.Combine(urlActual, contenedor, nombreArchivo.Replace("\\", "/"));

            return urlParaBd;
        }
    }
}
