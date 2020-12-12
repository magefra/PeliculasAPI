using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Helpers
{
    public static class HttpContextExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <param name="queryable"></param>
        /// <param name="cantidadRegistrosPagian"></param>
        /// <returns></returns>
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext,
                                                                 IQueryable<T> queryable,
                                                                 int cantidadRegistrosPagina)
        {
            double cantida = await queryable.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantida / cantidadRegistrosPagina);

            httpContext.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());

        }
    }
}
