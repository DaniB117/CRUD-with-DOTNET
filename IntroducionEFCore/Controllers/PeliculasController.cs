using AutoMapper;
using IntroducionEFCore.DTOs;
using IntroducionEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroducionEFCore.Controllers
{
    [ApiController]
    [Route("api/pelicula")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PeliculasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PeliculasCreacionDTO peliculaCreacion)
        {
            var pelicula = _mapper.Map<Pelicula>(peliculaCreacion);
            
            if(pelicula.Generos is not null)
            {
                foreach(var genero in pelicula.Generos)
                {
                    _context.Entry(genero).State = EntityState.Unchanged;
                }
            }

            if(pelicula.PeliculasActores is not null)
            {
                for(int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }

            _context.Add(pelicula);
            await _context.SaveChangesAsync();
            return Ok();
        }
        //este get me recolecta toda la informacion
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Pelicula>>> Get(int id)
        {
            var pelicula = await _context.Peliculas.Where(a => a.Id == id)
                .Include(p => p.Comentarios)
                .Include(p => p.Generos)
                .Include(p => p.PeliculasActores.OrderBy(pa => pa.Orden))
                    .ThenInclude(pa => pa.Actor)
                .ToListAsync();

            if (pelicula.Count == 0)
            {
                return NotFound("Not found");
            }

            return pelicula;
        }

        [HttpGet("select/{id:int}")]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetSelect(int id)
        {
            var pelicula = await _context.Peliculas.Where(a => a.Id == id)
                .Select(pel => new
                {
                    pel.Id,
                    pel.Titulo,
                    Generos = pel.Generos.Select(g => g.Name).ToList(),
                    Actores = pel.PeliculasActores.OrderBy(pa => pa.Orden).Select(pa => new
                    {
                        pa.ActorId,
                        pa.Actor.Name,
                        pa.Personaje
                    }),
                    Comentarios = pel.Comentarios.Count()
                })
                .ToListAsync();

            if (pelicula.Count == 0)
            {
                return NotFound("Not found");
            }

            return Ok(pelicula); //porque estamos proyectando (select) un tipo anonimo.
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAlteradas = await _context.Peliculas.Where(g => g.Id == id).ExecuteDeleteAsync();
            if (filasAlteradas == 0)
            {
                return NotFound();
            }
            //204 ok pero no tenemos que retornar nada
            return NoContent();
        }



    }
}
