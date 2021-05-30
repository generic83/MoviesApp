using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data.Models.Entities;
using MoviesStore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ICollection<Movie>> GetMovies()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }
    }
}
