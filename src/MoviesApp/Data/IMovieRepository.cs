using MoviesApp.Data.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesStore.Data
{
    public interface IMovieRepository
    {
        IQueryable<Movie> GetAllAsQueryable();

        Task<Movie> GetByIdAsync(int id);

        Task<ICollection<string>> GetAllLanguagesAsync();

        Task<ICollection<string>> GetAllLocationsAsync();
    }
}