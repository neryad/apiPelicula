using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using APIPELICULA.DTOS;
using APIPELICULA.Entidades;
using APIPELICULA.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPELICULA.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivo _almacenadorArchivo;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivo 
            almacenadorArchivo )
        {
            _context = context;
            _mapper = mapper;
            _almacenadorArchivo = almacenadorArchivo;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculaDto>>> Get()
        {
            var pelicuas = await _context.Peliculas.ToListAsync();
            return _mapper.Map<List<PeliculaDto>>(pelicuas);
        }
        
        [HttpGet("{id}", Name = "obtenerpelicula")]
        public async Task<ActionResult<PeliculaDto>> Get(int id)
        {
            var pelicuas = await _context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (pelicuas == null)
            {
                return NotFound();
            }
            return _mapper.Map<PeliculaDto>(pelicuas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDto peliculaCreacionDto)
        {
            var pelicula = _mapper.Map<Pelicula>(peliculaCreacionDto);
           
            if (peliculaCreacionDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extesion = Path.GetExtension(peliculaCreacionDto.Poster.FileName);
                    pelicula.Poster = await _almacenadorArchivo.GuardarArchivo(contenido, extesion, contenedor,
                        peliculaCreacionDto.Poster.ContentType);
                }
            }
            asignarOrdenActores(pelicula);
            _context.Add(pelicula);
            await _context.SaveChangesAsync();
            var pelilaDto = _mapper.Map<PeliculaDto>(pelicula);
             return new CreatedAtRouteResult("obtenerpelicula", new {id = pelicula.Id}, pelilaDto);
        }

        private void asignarOrdenActores(Pelicula pelicula)
        {
            if (pelicula.PeliculasActoreses != null)
            {
                for (int i = 0; i < pelicula.PeliculasActoreses.Count; i++)
                {
                    pelicula.PeliculasActoreses[i].Orden = i;
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromForm] PeliculaCreacionDto peliculaCreacionDto)
        {
            var PeliculaDb = await _context.Peliculas
                .Include(x => x.PeliculasActoreses)
                .Include(x => x.PeliclasGeneroses)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (PeliculaDb == null)
            {
                return NotFound();
                
            }

            PeliculaDb = _mapper.Map(peliculaCreacionDto, PeliculaDb);
            if (peliculaCreacionDto.Poster != null)
            {
                using (var memoryStrema = new MemoryStream())
                {
                    await peliculaCreacionDto.Poster.CopyToAsync(memoryStrema);
                    var contenido = memoryStrema.ToArray();
                    var extesion = Path.GetExtension(peliculaCreacionDto.Poster.FileName);
                    PeliculaDb.Poster = await _almacenadorArchivo.EditarArchivo(contenido, extesion, contenedor,
                        PeliculaDb.Poster,peliculaCreacionDto.Poster.ContentType);
                }
            }

            asignarOrdenActores(PeliculaDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPachtDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entidadDb = await _context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDb == null)
            {
                return NotFound();
            }

            var entidadDto = _mapper.Map<PeliculaPachtDto>(entidadDb);
            patchDocument.ApplyTo(entidadDto,ModelState);

            var esValido = TryValidateModel(entidadDto);

            if (!esValido)
            {
                return BadRequest();
            }

            _mapper.Map(entidadDto, entidadDb);
            await _context.SaveChangesAsync();
            return NoContent();

        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Peliculas.AnyAsync(c => c.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Remove(new Pelicula() {Id = id});
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}