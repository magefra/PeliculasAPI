using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Data;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using PeliculasAPI.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;


        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        private readonly IAlmacenadorArchivos _almacenadorArchivos;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<PeliculasController> _logger;


        /// <summary>
        /// 
        /// </summary>
        private readonly string _contenedor = "peliculas";




        public PeliculasController(ApplicationDbContext applicationDbContext,
                                    IMapper mapper,
                                    IAlmacenadorArchivos almacenadorArchivos,
                                    ILogger<PeliculasController> logger)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _almacenadorArchivos = almacenadorArchivos;
            _logger = logger;
        }



        [HttpGet]
        public async Task<ActionResult<PeliculasIndexDTO>> Get()
        {

            var top = 5;
            var hoy = DateTime.Now;

            var proximosEstrenos = await _applicationDbContext.Peliculas
                                         .Where(x => x.FechaEstreno > hoy)
                                         .OrderBy(x => x.FechaEstreno)
                                         .Take(top)
                                         .ToListAsync();




            var enCines = await _applicationDbContext.Peliculas
                                .Where(x => x.EnCines)
                                .Take(top)
                                .ToListAsync();




            var resultado = new PeliculasIndexDTO();
            resultado.FuturosEstrenos = _mapper.Map<List<PeliculaDTO>>(proximosEstrenos);
            resultado.EnCines = _mapper.Map<List<PeliculaDTO>>(enCines);


            return resultado;
        }




        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculaDTO>>> Filtrar([FromQuery] FiltroPeliculasDTO filtroPeliculasDTO)
        {
            var peliculasQueryable = _applicationDbContext.Peliculas.AsQueryable();



            if (!string.IsNullOrEmpty(filtroPeliculasDTO.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.Titulo.Contains(filtroPeliculasDTO.Titulo));
            }


            if (filtroPeliculasDTO.Encines)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.EnCines);
            }

            if (filtroPeliculasDTO.ProximosEstrenos)
            {
                var hoy = DateTime.Now;

                peliculasQueryable = peliculasQueryable.Where(x => x.FechaEstreno > hoy);
            }

            if (filtroPeliculasDTO.GeneroId > 0)
            {
                peliculasQueryable = peliculasQueryable
                                     .Where(x => x.PeliculasGeneros.Select(y => y.GeneroId)
                                     .Contains(filtroPeliculasDTO.GeneroId));




            }



            await HttpContext.InsertarParametrosPaginacion(peliculasQueryable,
                                                           filtroPeliculasDTO.CantidadRegistrosPagina);


            if (!string.IsNullOrEmpty(filtroPeliculasDTO.CampoOrdernar))
            {
                var tipoOrden = filtroPeliculasDTO.OrdernAscente ? "ascending" : "descending";

                try
                {
                    peliculasQueryable = peliculasQueryable.OrderBy(
                                    $"{filtroPeliculasDTO.CampoOrdernar} {tipoOrden}");
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message, ex);

                }



            }

            var peliculas = await peliculasQueryable.Paginar(filtroPeliculasDTO.Paginacion).ToListAsync();

            return _mapper.Map<List<PeliculaDTO>>(peliculas);


        }


        [HttpGet("{id}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDetalleDTO>> Get(int id)
        {
            var pelicula = await _applicationDbContext.Peliculas
                                    .Include(x => x.PeliculaActores)
                                    .ThenInclude(x => x.Actor)
                                    .Include(x => x.PeliculasGeneros)
                                    .ThenInclude(x => x.Genero)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }



            pelicula.PeliculaActores = pelicula.PeliculaActores.OrderBy(x => x.Ordern).ToList();

            return _mapper.Map<PeliculaDetalleDTO>(pelicula);
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = _mapper.Map<Pelicula>(peliculaCreacionDTO);


            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);

                    pelicula.Poster = await _almacenadorArchivos.GuardarArchivo(contenido,
                                                                             extension,
                                                                             _contenedor,
                                                                             peliculaCreacionDTO.Poster.ContentType);
                }
            }


            AsignarOrdenActores(pelicula);

            _applicationDbContext.Add(pelicula);
            await _applicationDbContext.SaveChangesAsync();
            var peliculaDTO = _mapper.Map<PeliculaDTO>(pelicula);

            return new CreatedAtRouteResult("obtenerPelicula",
                                             new { id = pelicula.Id },
                                             peliculaDTO);
        }


        private void AsignarOrdenActores(Pelicula pelicula)
        {
            if (pelicula.PeliculaActores != null)
            {
                for (int i = 0; i < pelicula.PeliculaActores.Count; i++)
                {
                    pelicula.PeliculaActores[i].Ordern = i;
                }
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculaDB = await _applicationDbContext.Peliculas
                                    .Include(x => x.PeliculaActores)
                                    .Include(x => x.PeliculasGeneros)
                                    .FirstOrDefaultAsync(x => x.Id == id);



            if (peliculaDB == null)
            {
                return NotFound();
            }


            peliculaDB = _mapper.Map(peliculaCreacionDTO, peliculaDB);


            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);

                    peliculaDB.Poster = await _almacenadorArchivos.GuardarArchivo(contenido,
                                                                             extension,
                                                                             _contenedor,
                                                                             peliculaCreacionDTO.Poster.ContentType);
                }
            }


            AsignarOrdenActores(peliculaDB);
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();

        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPathDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }


            var entidadDB = await _applicationDbContext.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }


            var entidadDTO = _mapper.Map<PeliculaPathDTO>(entidadDB);

            patchDocument.ApplyTo(entidadDTO, ModelState);


            var esValido = TryValidateModel(entidadDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(entidadDTO, entidadDB);


            await _applicationDbContext.SaveChangesAsync();


            return NoContent();


        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _applicationDbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(new Pelicula()
            {
                Id = id
            });

            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
