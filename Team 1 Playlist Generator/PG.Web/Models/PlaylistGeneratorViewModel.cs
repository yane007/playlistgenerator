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

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        //TODO: ??? как да покажем червения текст за грешка
        //[DataType(DataType.Text)]
        //[Display(Name = "Playlist Name")]
        //[StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string PlaylistName { get; set; }


        public int Metal { get; set; }

        public int Rock { get; set; }

        public int Pop { get; set; }

        public bool TopTracks { get; set; }

        public bool SameArtist { get; set; }


    }
}
