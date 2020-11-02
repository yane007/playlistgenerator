using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Models
{
    public class User  // To have id we need to put Identity first 
    {
        public bool IsDeleted { get; set; }
        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
