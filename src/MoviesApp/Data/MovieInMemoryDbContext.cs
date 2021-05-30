using Microsoft.EntityFrameworkCore;
using MoviesApp.Data.Models.Entities;

namespace MoviesApp.Data
{
    public class MovieInMemoryDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieStill> Stills { get; set; }

        public MovieInMemoryDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
