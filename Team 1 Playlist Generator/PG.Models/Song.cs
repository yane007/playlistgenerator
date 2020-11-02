using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PG.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        public int Duration { get; set; }
        public int Rank { get; set; }
        public string Link { get; set; }
        public string PreviewUrl { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}