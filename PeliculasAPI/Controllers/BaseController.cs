using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Data;
using PeliculasAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _mapper;



        public BaseController(ApplicationDbContext applicationDbContext,
                                 IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntidad"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <returns></returns>
        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class
        {
            var entidades = await _applicationDbContext.Set<TEntidad>().AsNoTracking().ToListAsync();

            var dtos = _mapper.Map<List<TDTO>>(entidades);

            return dtos;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntidad"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId
        {
            var entidad = await _applicationDbContext.Set<TEntidad>()
                                                     .AsNoTracking()
                                                     .FirstOrDefaultAsync(x => x.Id == id);

            string hola = "";

            if(entidad == null)
            {
                return NotFound();
            }


            return _mapper.Map<TDTO>(entidad);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCreacion"></typeparam>
        /// <typeparam name="TEntidad"></typeparam>
        /// <typeparam name="TLectura"></typeparam>
        /// <param name="creacionDTO"></param>
        /// <param name="nombreRuta"></param>
        /// <returns></returns>
        protected async Task<ActionResult> Post<TCreacion,TEntidad,TLectura> 
         (TCreacion creacionDTO, string nombreRuta) where TEntidad : class , IId
        {
            var entidad = _mapper.Map<TEntidad>(creacionDTO);
            _applicationDbContext.Add(entidad);
            await _applicationDbContext.SaveChangesAsync();

            var generoDto = _mapper.Map<TLectura>(entidad);

            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, generoDto);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCreacion"></typeparam>
        /// <typeparam name="TEntidad"></typeparam>
        /// <param name="id"></param>
        /// <param name="creacion"></param>
        /// <returns></returns>
        protected async Task<ActionResult> Put<TCreacion,TEntidad>
            (int id, TCreacion creacionDTO) where TEntidad :class , IId
        {

            var entidad = _mapper.Map<TEntidad>(creacionDTO);
            entidad.Id = id;

            _applicationDbContext.Entry(entidad).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntidad"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<ActionResult> Delete<TEntidad>(int id)
            where TEntidad : class, IId, new ()
        {
            var existe = await _applicationDbContext.Set<TEntidad>().AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(new TEntidad()
            {
                Id = id
            });

            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }



    }
}
