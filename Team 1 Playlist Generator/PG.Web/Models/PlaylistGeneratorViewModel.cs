using PG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class PlaylistGeneratorViewModel
    {
        public PlaylistGeneratorViewModel()
        {
            ganres = new List<GenreViewModel>();
        }
        public List<GenreViewModel> ganres  { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public int metal { get; set; }
        public int rock { get; set; }
        public int pop { get; set; }
        public bool topTracks { get; set; }
        public bool sameArtist { get; set; }

    }
}
