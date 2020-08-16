using System.Collections.Generic;
using System.Threading.Tasks;
using APIPELICULA.DTOS;
using APIPELICULA.Entidades;
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
        public async Task<ActionResult<List<ActorDto>>> Get()
        {
            var entidades = await _context.Actores.ToListAsync();
            return _mapper.Map<List<ActorDto>>(entidades);
            
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDto>> Get(int id)
        {
            var entidad = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            return _mapper.Map<ActorDto>(entidad);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ActorCreacionDto actorCreacionDto)
        {
            var entidad = _mapper.Map<Actor>(actorCreacionDto);
            _context.Add(entidad);
            await _context.SaveChangesAsync();
            var dto = _mapper.Map<ActorDto>(entidad);
            return new CreatedAtRouteResult("obtenerActor", new {id = entidad.Id}, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActorCreacionDto actorCreacionDto)
        {
            var entidad = _mapper.Map<Actor>(actorCreacionDto);
            entidad.Id = id;
            _context.Entry(entidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Actores.AnyAsync(c => c.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Remove(new Actor() {Id = id});
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}