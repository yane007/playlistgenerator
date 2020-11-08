using PG.Services.DTOs.Abstract;

namespace PG.Services.DTOs
{
    public class ArtistDTO : EntityDTO
    {
        public string Name { get; set; }

        public string Tracklist { get; set; }

        public string Type { get; set; }
    }
}
