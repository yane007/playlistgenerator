using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class PlaylistsSongs
    {
        [Required]
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        [Required]
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
