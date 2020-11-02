using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Models
{
    public class User
    {
        public bool IsDeleted { get; set; }
        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
