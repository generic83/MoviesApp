using Microsoft.AspNetCore.Mvc;
using MoviesApp.Converters;
using MoviesApp.Data;
using MoviesApp.Data.Models;
using MoviesApp.Data.Models.Entities;
using MoviesApp.SystemIo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SeedController : ControllerBase
    {
        private readonly IJsonConvert _jsonConvert;
        private readonly IFile _file;
        private readonly MovieInMemoryDbContext _dbContext;

        public SeedController(IJsonConvert jsonConvert, IFile file, MovieInMemoryDbContext dbContext)
        {
            _jsonConvert = jsonConvert;
            _file = file;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            var existingMovies = _dbContext.Movies;
            if (existingMovies.Any())
            {
                _dbContext.Movies.RemoveRange(existingMovies);
                await _dbContext.SaveChangesAsync();
            }

            using var openStream = _file.OpenRead("Data/Source/movies.json");
            var data = _jsonConvert.DeserializeAsync<MovieJsonModel>(openStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            foreach (var item in data.Movies)
            {
                var movie = new Movie
                {
                    ImdbId = item.ImdbId,
                    ImdbRating = item.ImdbRating,
                    Language = item.Language,
                    Location = item.Location,
                    ListingType = item.ListingType,
                    Plot = item.Plot,
                    Poster = item.Poster,
                    MovieStills = item.Stills.Select(x => new MovieStill { Source = x }).ToArray(),
                    Title = item.Title,
                };

                foreach (var soundEffect in item.SoundEffects)
                {
                    var enumValue = (SoundEffectsEnum)Enum.Parse(typeof(SoundEffectsEnum), soundEffect);
                    movie.SoundEffectsEnum |= enumValue;
                }

                _dbContext.Movies.Add(movie);
            }
            await _dbContext.SaveChangesAsync();

            return new JsonResult(new
            {
                Movies = _dbContext.Movies.Count()
            });
        }
    }
}
