using Newtonsoft.Json;
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

        [MaxLength(800)]
        public string Description { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public int Fans { get; set; }

        [MaxLength(300)]
        public string Link { get; set; }

        [MaxLength(300)]
        public string Share { get; set; }

        [MaxLength(300)]
        public string Picture { get; set; }

        [MaxLength(300)]
        public string Tracklist { get; set; }

        [MaxLength(300)]
        public string Creation_date { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; } = new List<PlaylistsSongs>();

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public int UserId { get; set; }

        public User User { get; set; }


    }
}