using PG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.DTOs
{
    public class SongDTO
    {
        public int Id { get; set; } 
        public bool Readable { get; set; } 
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
        public string Type { get; set; }
        public int ArtistId { get; set; } 
        public int AlbumId { get; set; }
        public int PlaylistId { get; set; }

    }
}
