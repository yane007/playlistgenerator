using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PG.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(200)]
        public string Title { get; set; }

        public string Link { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public string Preview { get; set; }

        public string Type { get; set; }


        public Creator Artist { get; set; }
        public int ArtistId { get; set; }
            

        //public Album Album { get; set; }
        //public int AlbumId { get; set; }

        public Playlist Playlist { get; set; }
        public int? PlaylistId { get; set; }

        public Genre Genre { get; set; }
        public int GenreId { get; set; }
    }
}