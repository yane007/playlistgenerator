

namespace PG.Services.MappingModelsAPI
{
    public class SongAPI
    {
        public string title { get; set; }
        public string link { get; set; }
        public int duration { get; set; }
        public int rank { get; set; }
        public string preview { get; set; }
        public ArtistAPI artist { get; set; }
        public int genreId { get; set; }
    }
}
