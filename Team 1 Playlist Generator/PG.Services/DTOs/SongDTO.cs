using PG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.DTOs
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Duration { get; set; }
        public int Rank { get; set; }
        public string Link { get; set; }
        public string PreviewUrl { get; set; }

        public int ArtistId { get; set; }
        public Creator Creator{ get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
