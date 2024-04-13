using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace IntroducionEFCore.Entidades.Configuraciones
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            //esto se podria hacer pero en su propia clase
            //modelBuilder.Entity<Genero>().HasKey(e => e.Id);
            //modelBuilder.Entity<Genero>().Property(e => e.Name).HasMaxLength(150);
            //modelBuilder.Entity<Actor>().Property(a => a.Name).HasMaxLength(150);
            builder.Property(a => a.FechaNacimiento).HasColumnType("date");
            builder.Property(a => a.Fortuna).HasPrecision(18, 2);
        }
    }
}
