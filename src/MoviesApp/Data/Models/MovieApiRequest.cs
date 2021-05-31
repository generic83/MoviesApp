using Microsoft.AspNetCore.Mvc;

namespace MoviesApp.Data.Models
{
    public class MovieApiRequest
    {
        [FromQuery]
        public int PageIndex { get; set; } = 0;

        [FromQuery]
        public int PageSize { get; set; } = 5;

        [FromQuery]
        public string SortColumn { get; set; } = null;

        [FromQuery]
        public string SortOrder { get; set; } = null;

        [FromQuery]
        public string FilterQuery { get; set; } = null;

        [FromQuery]
        public string Language { get; set; } = null;

        [FromQuery]
        public string Location { get; set; } = null;
    }
}
