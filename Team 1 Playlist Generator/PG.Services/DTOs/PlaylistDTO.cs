using PG.Models;
using PG.Services.DTOs.Abstract;
using System.Collections.Generic;

namespace PG.Services.DTOs
{
    public class PlaylistDTO : EntityDTO
    {
        public string Title { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public PixabayImageDTO PixabayImage { get; set; }

        public string UserId { get; set; }

        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; } = new List<PlaylistsSongs>();
    }
}
