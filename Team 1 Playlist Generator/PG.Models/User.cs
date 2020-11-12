using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
