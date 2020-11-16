using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class PlaylistsGenres
    {
        [Required]
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
