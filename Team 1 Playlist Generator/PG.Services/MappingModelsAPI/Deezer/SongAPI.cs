using PG.Services.MappingModelsAPI.Deezer;

namespace PG.Services.MappingModelsAPI
{
    public class SongAPI
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public string Preview { get; set; }

        public ArtistAPI Artist { get; set; }

        public int GenreId { get; set; }

        public AlbumAPI Album { get; set; }
    }
}
