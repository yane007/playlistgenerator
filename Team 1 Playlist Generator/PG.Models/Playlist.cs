using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Playlist : Entity
    {

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public int Duration { get; set; }

        [MaxLength(300)]
        public string Picture { get; set; }

        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; } = new List<PlaylistsSongs>();

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public int UserId { get; set; }

        public User User { get; set; }


    }
}