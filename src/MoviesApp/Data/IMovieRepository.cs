using MoviesApp.Data.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesStore.Data
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetAllMoviesAsync();
    }
}