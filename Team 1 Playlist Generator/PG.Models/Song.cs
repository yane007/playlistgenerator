using Newtonsoft.Json;
using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PG.Models
{
    public class Song : Entity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        [MaxLength(300)]
        public string Preview { get; set; }


        public Artist Artist { get; set; }
        public int ArtistId { get; set; }


        public Genre Genre { get; set; }
        public int GenreId { get; set; }


        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; } = new List<PlaylistsSongs>();
    }
}