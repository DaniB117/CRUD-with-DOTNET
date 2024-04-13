using System.ComponentModel.DataAnnotations;

namespace IntroducionEFCore.DTOs
{
    public class GeneroCreacionDTO
    {
        [StringLength(maximumLength: 150)]
        public string Name { get; set; } = null!;
    }
}
