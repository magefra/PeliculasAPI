using System.Threading.Tasks;

namespace PeliculasAPI.Servicios
{
    public interface IAlmacenadorArchivos
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contenido"></param>
        /// <param name="extension"></param>
        /// <param name="contenedor"></param>
        /// <param name="ruta"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<string> EditarArchivo(byte[] contenido,
                                   string extension,
                                   string contenedor,
                                   string ruta, 
                                   string contentType);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="contenedor"></param>
        /// <returns></returns>
        Task BorrarArchivo(string ruta, string contenedor);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="contenido"></param>
        /// <param name="extension"></param>
        /// <param name="contenedor"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<string> GuardarArchivo(byte[] contenido, 
                                    string extension, 
                                    string contenedor, 
                                    string contentType);
    }
}
