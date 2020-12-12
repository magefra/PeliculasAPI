using PeliculasAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Helpers
{
    public static class QueryableExtension
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="paginacionDTO"></param>
        /// <returns></returns>
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, 
                                               PaginacionDTO paginacionDTO)
        {

            return queryable.Skip((paginacionDTO.Pagina -1) * paginacionDTO.CantidadRegistroPagina)
                            .Take(paginacionDTO.CantidadRegistroPagina);
        }
    }
}
