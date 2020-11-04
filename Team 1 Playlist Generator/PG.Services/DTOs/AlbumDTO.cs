using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.DTOs
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Cover_small { get; set; }
        public string Cover_medium { get; set; }
        public string Cover_big { get; set; }
        public string Cover_xl { get; set; }
        public string Md5_image { get; set; }
        public string Tracklist { get; set; }
        public string Type { get; set; }
    }
}
