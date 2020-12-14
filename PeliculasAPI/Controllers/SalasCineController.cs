using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Data;
using PeliculasAPI.DTOs.SalaCine;
using PeliculasAPI.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/SalasCine")]
    public class SalasCineController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;


        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _mapper;



        public SalasCineController(ApplicationDbContext applicationDbContext,
                                 IMapper mapper)
         : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<SalaCineDTO>>> Get()
        {
            return await Get<SalaCine, SalaCineDTO>();
        }


        [HttpGet("{id:int}", Name = "obtenerSalaCine")]
        public async Task<ActionResult<SalaCineDTO>> Get(int id)
        {
            return await Get<SalaCine, SalaCineDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalaCineCreacionDTO salaCineCreacionDTO)
        {
            return await Post<SalaCineCreacionDTO, SalaCine, SalaCineDTO>(salaCineCreacionDTO, "obtenerSalaCine");
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody]SalaCineCreacionDTO salaCineCreacionDTO)
        {
            return await Put<SalaCineCreacionDTO, SalaCine>(id, salaCineCreacionDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<SalaCine>(id);
        }


    }
}

