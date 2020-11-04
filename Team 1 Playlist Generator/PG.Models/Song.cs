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
        public bool Readable { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }
        public string Title_short { get; set; }
        public string Title_version { get; set; }
        public string Link { get; set; }
        public int Duration { get; set; }
        public int Rank { get; set; }
        public bool Explicit_lyrics { get; set; }
        public int Explicit_content_lyrics { get; set; }
        public int Explicit_content_cover { get; set; }
        public string Preview { get; set; }
        public string Md5_image { get; set; }
        public int Time_add { get; set; }
        public Creator Artist { get; set; }
        public Album Album { get; set; } 
        public string Type { get; set; }

        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
    }
}