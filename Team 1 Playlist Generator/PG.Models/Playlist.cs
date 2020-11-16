using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Playlist : Entity
    {
        public Playlist()
        {
            PlaylistsSongs = new List<PlaylistsSongs>();
            Genres = new List<Genre>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public int Duration { get; set; }

        [MaxLength(300)]
        public string Picture { get; set; }

        public int Rank { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; }

        public ICollection<PlaylistsGenres> PlaylistsGenres { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}