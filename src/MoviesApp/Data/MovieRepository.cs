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
    }
}
