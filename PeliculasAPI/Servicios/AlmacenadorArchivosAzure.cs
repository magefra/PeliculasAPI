using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PeliculasAPI.Servicios
{
    public class AlmacenadorArchivosAzure : IAlmacenadorArchivos
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _connectionString;



        public AlmacenadorArchivosAzure(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="contenedor"></param>
        /// <returns></returns>
        public async Task BorrarArchivo(string ruta, string contenedor)
        {
            if (ruta != null)
            {
                var cuenta = CloudStorageAccount.Parse(_connectionString);
                var cliente = cuenta.CreateCloudBlobClient();
                var contenedorReferencia = cliente.GetContainerReference(contenedor);

                var nombreBlob = Path.GetFileName(ruta);
                var blob = contenedorReferencia.GetBlockBlobReference(nombreBlob);

                await blob.DeleteIfExistsAsync();

            }
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
        public async Task<string> GuardarArchivo(byte[] contenido,
                                                 string extension,
                                                 string contenedor,
                                                 string contentType)
        {
            var cuenta = CloudStorageAccount.Parse(_connectionString);
            var cliente = cuenta.CreateCloudBlobClient();
            var contenedorReferencia = cliente.GetContainerReference(contenedor);


            await contenedorReferencia.CreateIfNotExistsAsync();
            await contenedorReferencia.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var blob = contenedorReferencia.GetBlockBlobReference(nombreArchivo);


            await blob.UploadFromByteArrayAsync(contenido, 0, contenido.Length);

            blob.Properties.ContentType = contentType;

            await blob.SetPropertiesAsync();

            return blob.Uri.ToString();

        }
    }
}
