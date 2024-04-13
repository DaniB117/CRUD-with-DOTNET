using IntroducionEFCore.Entidades;
using IntroducionEFCore.Entidades.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IntroducionEFCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Api fluent agregar configuración personalizada del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedingInical.Seed(modelBuilder);
            //Asi es como se hacen los atributos aqui
            //modelBuilder.Entity<Comentario>().Property(a => a.Contenido).HasMaxLength(500);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }

        public DbSet<Genero> Generos => Set<Genero>();

        public DbSet<Actor> Actores => Set<Actor>();
        
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();

        public DbSet<Comentario> Comentarios => Set<Comentario>();

        public DbSet<PeliculaActor> PeliculasActores => Set<PeliculaActor>();
    }
}
