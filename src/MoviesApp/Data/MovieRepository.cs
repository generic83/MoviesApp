using Microsoft.EntityFrameworkCore;
using MoviesApp.Data.Models.Entities;
using MoviesStore.Data;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Movie> GetAllAsQueryable()
        {
            return _dbContext.Movies.AsNoTracking().AsQueryable();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _dbContext.Movies.AsNoTracking().Include(x => x.MovieStills).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<string>> GetAllLocationsAsync()
        {
            return await _dbContext.Movies.AsNoTracking().Select(x => x.Location.ToLowerInvariant()).Distinct().ToListAsync();
        }

        public async Task<ICollection<string>> GetAllLanguagesAsync()
        {
            return await _dbContext.Movies.AsNoTracking().Select(x => x.Language.ToLowerInvariant()).Distinct().ToListAsync();
        }
    }
}
