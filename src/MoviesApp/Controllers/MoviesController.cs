using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data.Models;
using MoviesStore.Data;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _repository = movieRepository;
        }

        [HttpGet]
        [Route("{pageIndex?}/{pageSize?}")]
        public async Task<ActionResult<MovieApiResult>> GetMovies(int pageIndex = 0, int pageSize = 10, string sortColumn = null, string sortOrder = null)
        {

            return await MovieApiResult.CreateAsync(_repository.GetAllAsQueryable(), pageIndex, pageSize, sortColumn, sortOrder);
        }
    }
}
