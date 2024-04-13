using System.ComponentModel.DataAnnotations;

namespace IntroducionEFCore.DTOs
{
    public class ActorCreacionDTO
    {
        [StringLength(maximumLength:150)]
        public string Name { get; set; } = null!;
        public decimal Fortuna { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
