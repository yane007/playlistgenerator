using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class SongViewModel
    {
        public SongViewModel()
        {

        }
        public SongViewModel(SongDTO song)
        {
            Id = song.Id;
            Title = song.Title;
            Link = song.Link;
            Duration = song.Duration;
            Rank = song.Rank;
            Preview = song.Preview;
            ArtistId = song.ArtistId;
            GenreId = song.GenreId;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public string Preview { get; set; }

        public int ArtistId { get; set; }

        public int GenreId { get; set; }
    }
}