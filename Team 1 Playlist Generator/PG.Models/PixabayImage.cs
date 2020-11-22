using PG.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Models
{
    public class PixabayImage : Entity
    {
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        public string PreviewURL { get; set; }

        public string WebformatURL { get; set; }

        public string LargeImageURL { get; set; }
    }
}
