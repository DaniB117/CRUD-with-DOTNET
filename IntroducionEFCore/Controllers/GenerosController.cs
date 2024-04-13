using AutoMapper;
using IntroducionEFCore.DTOs;
using IntroducionEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace IntroducionEFCore.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(GeneroCreacionDTO generoCreacion)
        {
            var yaExiste = await _context.Generos.AnyAsync(g => g.Name == generoCreacion.Name);
            if (yaExiste)
            {
                return BadRequest("Already created");
            }

            var genero = _mapper.Map<Genero>(generoCreacion);
            //genero se tiene que mapear porque no es una entidad (Dbset)
            //var genero = new Genero
            //{
            //    Name = generoCreacion.Name
            //}; (forma de mapear)
            _context.Add(genero);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(GeneroCreacionDTO[] generosCreacionDTO)
        {
            var generos = _mapper.Map<Genero[]>(generosCreacionDTO);
            _context.AddRange(generos);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            
            var genero = await _context.Generos
                .Select(gen => new
                {
                    gen.Id,
                    gen.Name,
                    Peliculas = gen.Peliculas.Select(pel => pel.Titulo).ToList(),
                })
                .ToListAsync();
            return Ok(genero);
        }
        
        //metodo conectado
        [HttpPut("{id:int}/nombre2")]
        public async Task<ActionResult> Put(int id)
        {
            var genero = await _context.Generos.FirstOrDefaultAsync(g => g.Id == id);

            if(genero is null)
            {
                return NotFound();
            }

            genero.Name = genero.Name + "2";

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = _mapper.Map<Genero>(generoCreacionDTO);
            genero.Id = id;
            _context.Update(genero);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAlteradas = await _context.Generos.Where(g => g.Id == id).ExecuteDeleteAsync();
            if ( filasAlteradas == 0)
            {
                return NotFound();
            }
            //204 ok pero no tenemos que retornar nada
            return NoContent();
        }

        [HttpDelete("{id:int}/Anterior")]
        public async Task<ActionResult> DeleteAnterior(int id)
        {
            var genero = await _context.Generos.Where(g => g.Id == id).ToListAsync();
            if (genero.Count == 0)
            {
                return NotFound();
            }
            //Se puede hacer algo antes de borrar

            _context.Remove(genero);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
