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
    public class ActoresController : BaseController
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
            : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _almacenadorArchivos = almacenadorArchivos;
        }


        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {

            return await Get<Actor, ActorDTO>(paginacionDTO);

        }


        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            return await Get<Actor, ActorDTO>(id);
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
            return await Delete<Actor>(id);
        }




        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPathDTO> patchDocument)
        {
            return await Patch<Actor, ActorPathDTO>(id, patchDocument);

        }

    }
}
