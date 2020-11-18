using System.Collections.Generic;

namespace PG.Web.Models
{
    public class PlaylistGeneratorViewModel
    {
        public PlaylistGeneratorViewModel()
        {
            Genres = new List<GenreViewModel>();
        }

        public List<GenreViewModel> Genres { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public string PlaylistName { get; set; }

        public int Metal { get; set; }

        public int Rock { get; set; }

        public int Pop { get; set; }

        public bool TopTracks { get; set; }

        public bool SameArtist { get; set; }


    }
}
