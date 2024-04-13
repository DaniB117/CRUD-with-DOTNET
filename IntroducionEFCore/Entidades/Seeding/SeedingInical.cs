using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntroducionEFCore.Entidades.Seeding
{
    public class SeedingInical
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var samuelLJackson = new Actor()
            {
                Id = 3,
                Name = "Samuel L. Jackson",
                FechaNacimiento = new DateTime(1948, 12, 21),
                Fortuna = 20000
            };
            var robertDowneyJunior = new Actor()
            {
                Id = 4,
                Name = "Robert Downey Jr.",
                FechaNacimiento = new DateTime(1965, 4, 4),
                Fortuna = 25000
            };

            modelBuilder.Entity<Actor>().HasData(samuelLJackson, robertDowneyJunior);

            var avengers = new Pelicula()
            {
                Id = 2,
                Titulo = "Avengers Endgame",
                FechaEstreno = new DateTime(2019, 4, 22)
            };
            var spiderman = new Pelicula()
            {
                Id = 3,
                Titulo = "Spider-Man: Across the Spider-Verse (part one)",
                FechaEstreno = new DateTime(2022, 10, 7)
            };
            var pulpfiction = new Pelicula()
            {
                Id = 4,
                Titulo = "Pulp Fiction",
                FechaEstreno = new DateTime(1995, 1, 23)
            };

            modelBuilder.Entity<Pelicula>().HasData(avengers, spiderman, pulpfiction);

            var tablaGeneroPelicula = "GeneroPelicula";
            var generoIdPropiedad = "GenerosId";
            var peliculaIdPropiedad = "PeliculasId";

            var accion = 1;
            var drama = 3;
            var cienciaFiccion = 5;
            var animacion = 6;

            modelBuilder.Entity(tablaGeneroPelicula).HasData(
                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = cienciaFiccion,
                    [peliculaIdPropiedad] = avengers.Id
                },
                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = accion,
                    [peliculaIdPropiedad] = avengers.Id
                },
                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = animacion,
                    [peliculaIdPropiedad] = spiderman.Id
                },
                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = drama,
                    [peliculaIdPropiedad] = pulpfiction.Id
                }
                );

            var samuelAvengers = new PeliculaActor
            {
                ActorId = samuelLJackson.Id,
                PeliculaId = avengers.Id,
                Orden = 1,
                Personaje = "Nick Fury"
            };

            var samuelPulp = new PeliculaActor
            {
                ActorId = samuelLJackson.Id,
                PeliculaId = pulpfiction.Id,
                Orden = 2,
                Personaje = "Jules Winnfield"
            };

            var robertAvengers = new PeliculaActor
            {
                ActorId = robertDowneyJunior.Id,
                PeliculaId = avengers.Id,
                Orden = 1,
                Personaje = "Tony Stark"
            };

            modelBuilder.Entity<PeliculaActor>().HasData(samuelAvengers, robertAvengers, samuelPulp);
        }
    }
}
