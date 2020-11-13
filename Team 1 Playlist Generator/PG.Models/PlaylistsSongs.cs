using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class PlaylistsSongs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        [Required]
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
