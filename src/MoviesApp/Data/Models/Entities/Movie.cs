using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApp.Data.Models.Entities
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ImdbId { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Location { get; set; }

        public string Plot { get; set; }

        public string Poster { get; set; }

        public string ListingType { get; set; }

        public string ImdbRating { get; set; }

        public SoundEffectsEnum SoundEffectsEnum { get; set; }

        public ICollection<MovieStill> MovieStills { get; set; }
    }
}
