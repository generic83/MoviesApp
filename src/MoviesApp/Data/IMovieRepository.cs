using MoviesApp.Data.Models.Entities;
using System.Linq;

namespace MoviesStore.Data
{
    public interface IMovieRepository
    {
        IQueryable<Movie> GetAllAsQueryable();
    }
}