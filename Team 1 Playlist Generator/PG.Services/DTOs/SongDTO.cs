using PG.Services.DTOs.Abstract;

namespace PG.Services.DTOs
{
    public class SongDTO : EntityDTO
    {
        public int DeezerID { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public string Preview { get; set; }

        public int? ArtistId { get; set; }

        public int? GenreId { get; set; }

        public int? AlbumId { get; set; }
    }
}
