using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PG.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(30)]
        public string Name { get; set; }
        public string ArtistTrackListURL { get; set; }
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
