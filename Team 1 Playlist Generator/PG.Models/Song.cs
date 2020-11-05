using Newtonsoft.Json;
using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PG.Models
{
    public class Song : IdAndIsDeleted
    {
        [Required]
        [MinLength(3), MaxLength(200)]
        public string Title { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public string Preview { get; set; }


        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
            

        public Genre Genre { get; set; }
        public int GenreId { get; set; }


        public ICollection<PlaylistAndSongRelation> PlaylistAndSongRelation { get; set; } = new List<PlaylistAndSongRelation>();
    }
}