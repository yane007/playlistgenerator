using System;
using System.Collections.Generic;
using System.Text;
using PG.Models;

namespace PG.Services.DTOs
{
    public class PlaylistDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int Rank { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();

    }
}
