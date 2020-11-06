using PG.Models;
using PG.Services.DTOs.Abstract;

namespace PG.Services.DTOs
{
    public class PlaylistDTO : IdAndDeletedDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int Fans { get; set; }
        public string Link { get; set; }
        public string Share { get; set; }
        public string Picture { get; set; }
        public string Tracklist { get; set; }
        public string Creation_date { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
    }
}
