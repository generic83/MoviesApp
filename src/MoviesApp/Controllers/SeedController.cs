using Microsoft.AspNetCore.Mvc;
using MoviesApp.Converters;
using MoviesApp.Data.Models;
using MoviesApp.SystemIo;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SeedController : ControllerBase
    {
        private readonly IJsonConvert _jsonConvert;
        private readonly IFile _file;

        public SeedController(IJsonConvert jsonConvert, IFile file)
        {
            _jsonConvert = jsonConvert;
            _file = file;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            using var openStream = _file.OpenRead("Data/Source/movies.json");
            var data = await _jsonConvert.DeserializeAsync<ICollection<MovieJsonModel>>(openStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            //TODO store deserialized data in an in-memory db

            return new JsonResult(new
            {
                Movies = data.Count
            });
        }
    }
}
