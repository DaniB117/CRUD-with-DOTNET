using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroducionEFCore.DTOs
{
    public class PeliculasCreacionDTO
    {
        [StringLength(150)]
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; } 
        public DateTime FechaEstreno { get; set; }
        public List<int> Generos { get; set; } = new List<int>();
        public List<PeliculaActorCreacionDTO> PeliculasActores { get; set; }
            = new List<PeliculaActorCreacionDTO>();
    }
}
