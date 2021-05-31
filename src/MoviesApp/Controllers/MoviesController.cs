using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data.Models;
using MoviesApp.Data.Models.Entities;
using MoviesStore.Data;
using System.Collections.Generic;
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
        [Route("{pageIndex?}/{pageSize?}/{sortColumn?}/{sortOrder?}/{filterQuery?}/{language?}/{location?}")]
        public async Task<ActionResult<MovieApiResult>> GetMovies([FromRoute]MovieApiRequest request)
        {
           return await MovieApiResult.CreateAsync(_repository.GetAllAsQueryable(), request);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _repository.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpGet]
        [Route("AvailableLanguages")]
        public async Task<ICollection<string>> GetAllLanguages()
        {
            return await _repository.GetAllLanguagesAsync();
        }

        [HttpGet]
        [Route("AvailableLocations")]
        public async Task<ICollection<string>> GetAllLocations()
        {
            return await _repository.GetAllLocationsAsync();
        }
    }
}
