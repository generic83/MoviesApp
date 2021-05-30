using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Data.Models.Entities
{
    public class MovieStill
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        [MaxLength(256)]
        public string Source { get; set; }
    }
}
