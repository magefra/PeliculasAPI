using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Data;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using PeliculasAPI.Migrations;
using PeliculasAPI.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
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
        private readonly string _contenedor = "actores";


        public ActoresController(ApplicationDbContext applicationDbContext,
                                 IMapper mapper,
                                 IAlmacenadorArchivos almacenadorArchivos)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _almacenadorArchivos = almacenadorArchivos;
        }


        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {

            var queryable = _applicationDbContext.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistroPagina);


            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
            return _mapper.Map<List<ActorDTO>>(entidades);

        }


        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var entidad = await _applicationDbContext.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            return _mapper.Map<ActorDTO>(entidad);
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var entidad = _mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);

                    entidad.Foto = await _almacenadorArchivos.GuardarArchivo(contenido,
                                                                             extension,
                                                                             _contenedor,
                                                                             actorCreacionDTO.Foto.ContentType);


                }
            }


            _applicationDbContext.Add(entidad);
            await _applicationDbContext.SaveChangesAsync();

            var dto = _mapper.Map<ActorDTO>(entidad);


            return new CreatedAtRouteResult("obtenerActor", new { id = entidad.Id }, dto);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {

            var actorDb = await _applicationDbContext.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actorDb == null)
            {
                return NotFound();
            }

            actorDb = _mapper.Map(actorCreacionDTO, actorDb);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);

                    actorDb.Foto = await _almacenadorArchivos.EditarArchivo(contenido,
                                                                             extension,
                                                                             _contenedor,
                                                                             actorDb.Foto,
                                                                             actorCreacionDTO.Foto.ContentType);


                }
            }



            await _applicationDbContext.SaveChangesAsync();
            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _applicationDbContext.Actores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(new Actor()
            {
                Id = id
            });

            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }




    [HttpPatch("{id}")]
    public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPathDTO> patchDocument)
    {
        if (patchDocument == null)
        {
            return BadRequest();
        }


            var entidadDB = await _applicationDbContext.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if(entidadDB == null)
            {
                return NotFound();
            }


            var entidadDTO = _mapper.Map<ActorPathDTO>(entidadDB);

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

}
}
