using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Data;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _mapper;



        public GenerosController(ApplicationDbContext applicationDbContext,
                                 IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {

            var entidades = await _applicationDbContext.Generos.ToListAsync();

            var generoDto = _mapper.Map<List<GeneroDTO>>(entidades);


            return generoDto;
        }


        [HttpGet("{id:int}", Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            var entidad = await _applicationDbContext.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            var genero = _mapper.Map<GeneroDTO>(entidad);

            return genero;

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var entidad = _mapper.Map<Genero>(generoCreacionDTO);
            _applicationDbContext.Add(entidad);
            await _applicationDbContext.SaveChangesAsync();

            var generoDto = _mapper.Map<GeneroDTO>(entidad);

            return new CreatedAtRouteResult("obtenerGenero", new { id = generoDto.Id }, generoDto);
        } 

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var entidad = _mapper.Map<Genero>(generoCreacionDTO);
            entidad.Id = id;

            _applicationDbContext.Entry(entidad).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _applicationDbContext.Generos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(new Genero()
            {
                Id = id
            });

            await _applicationDbContext.SaveChangesAsync();

            return NoContent();

        }

    }
}
