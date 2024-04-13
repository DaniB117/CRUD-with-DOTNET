using System.ComponentModel.DataAnnotations; // este es para poder colocar atributos a las propiedades

namespace IntroducionEFCore.Entidades
{
    //Para generar la migration es con Add-migration 'nombre de migracion' en el  Package Manager Console
    //Despues se carga a la base de datos con update database
    public class Genero
    {
        //[Key] para hacer que sea llave primaria pero por automatico es PK cuando se llama Id
        public int Id { get; set; }
        //[StringLength(maximumLength: 150)]
        public string Name { get; set; } = null!;
        public HashSet<Pelicula> Peliculas { get; set; } = new HashSet<Pelicula>();
    }
}
