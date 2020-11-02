using PG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.DTOs
{
    public class ArtistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
