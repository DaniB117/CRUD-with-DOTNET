using AutoMapper;
using IntroducionEFCore.DTOs;
using IntroducionEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace IntroducionEFCore.Controllers
{
    [ApiController]
    [Route("api/peliculas/{peliculaId:int}/comentarios")]
    public class ComentariosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int peliculaId, ComentarioCreacionDTO comentarioCreacion)
        {
            var comentario = _mapper.Map<Comentario>(comentarioCreacion);
            comentario.PeliculaId = peliculaId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            return Ok();
        } 
    }
}
