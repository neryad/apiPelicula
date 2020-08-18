using APIPELICULA.Entidades;
using Microsoft.EntityFrameworkCore;

namespace APIPELICULA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)    
        {
            
        }

        public DbSet<Genero> Generos { get; set; }    
        public DbSet<Actor> Actores { get; set; }
        
        public DbSet<Pelicula> Peliculas { get; set; }
    }
}