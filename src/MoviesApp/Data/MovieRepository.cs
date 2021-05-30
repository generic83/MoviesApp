using Microsoft.EntityFrameworkCore;
using MoviesApp.Data.Models.Entities;
using MoviesStore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApp.Data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieInMemoryDbContext _dbContext;

        public MovieRepository(MovieInMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Movie>> GetAllMoviesAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }
    }
}
