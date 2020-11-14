using PG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class PlaylistViewModel
    {
        public string Title { get; set; }

        public int Duration { get; set; }

        public int Fans { get; set; }

        public string Link { get; set; }

        public string Share { get; set; }

        public string Picture { get; set; }

        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; } = new List<PlaylistsSongs>();
    }
}
