namespace PG.Web.APIControllers.Models
{
    public class PlaylistGeneratorAPI
    {
        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public string PlaylistName { get; set; }

        public int Metal { get; set; }

        public int Rock { get; set; }

        public int Pop { get; set; }

        public int Chalga { get; set; }

        public bool TopTracks { get; set; }

        public bool SameArtist { get; set; }
    }
}
