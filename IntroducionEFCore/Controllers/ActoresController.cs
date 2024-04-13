using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntroducionEFCore.DTOs;
using IntroducionEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroducionEFCore.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public ActoresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> Post(ActorCreacionDTO actorCreacionDTO)
        {
            var actors = _mapper.Map<Actor>(actorCreacionDTO);
            _context.Add(actors);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            return await _context.Actores
                .OrderBy(a => a.FechaNacimiento)
                    .ThenBy(a => a.Fortuna)
                .ToListAsync();
        }

        [HttpGet("name")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(string name)
        {
            //return await _context.Actores.Where(a => a.Name==name).ToListAsync();
            return await _context.Actores.Where(a => a.Name.Contains(name)).ToListAsync();
        }

        [HttpGet("fechanacimiento/rango")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(int inicio, int fin)
        {
            DateTime fechainicio = new DateTime(inicio, 1, 1);
            DateTime fechafin = new DateTime(fin, 12, 31);
            return await _context.Actores
                .Where(a => a.FechaNacimiento >= fechainicio && a.FechaNacimiento <=fechafin)
                .ToListAsync();
        }

        [HttpGet("{id=int}")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(int id)
        {
            
            var actor = await _context.Actores.Where(a => a.Id == id).ToListAsync();

            if (actor.Count == 0)
            {
                return NotFound("{message: not found}");
            }

            return actor;
        }
        [HttpGet("idnombre")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> Getidnombre()
        {
            //return await _context.Actores.Select(a => new ActorDTO {Id =a.Id,Name =a.Name}).ToListAsync();
            return await _context.Actores
                .ProjectTo<ActorDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

        }

    }
}
