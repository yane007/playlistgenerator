using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Web.Models
{
    public class PlaylistGeneratorViewModel
    {
        public PlaylistGeneratorViewModel()
        {
            Genres = new List<GenreViewModel>();
        }

        public List<GenreViewModel> Genres { get; set; }
        [Required]
        public string StartLocation { get; set; }
        [Required]
        public string EndLocation { get; set; }

        [Required]
        public string PlaylistName { get; set; }

        public int Metal { get; set; }

        public int Rock { get; set; }

        public int Pop { get; set; }

        public bool TopTracks { get; set; }

        public bool SameArtist { get; set; }
    }
}
