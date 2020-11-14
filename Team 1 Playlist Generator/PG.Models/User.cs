using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PG.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Playlists = new HashSet<Playlist>();
        }

        public bool IsDeleted { get; set; }
        public ICollection<Playlist> Playlists { get; set; }


    }
}
