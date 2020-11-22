using PG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class PlaylistViewModel
    {
        public int Id { get; set; }

        public List<int> DeezerIDs { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public string DurationInHours { get; set; }

        public int Fans { get; set; }

        public PixabayImageViewModel PixabayImage { get; set; }

        public string UserId { get; set; }

        public ICollection<SongViewModel> Songs { get; set; } = new List<SongViewModel>();
    }
}
