using System.Collections.Generic;
using System.Threading.Tasks;
using APIPELICULA.DTOS;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPELICULA.Controllers
{
    [ApiController]
    [Route("api/acotres")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActoresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var entidades = await _context.Actores.ToListAsync();
            return _mapper.Map<List<ActorDTO>>(entidades);
            
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var entidad = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            return _mapper.Map<ActorDTO>(entidad);
        }
    }
}