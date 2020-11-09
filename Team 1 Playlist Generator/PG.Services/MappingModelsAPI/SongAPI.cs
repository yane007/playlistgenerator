

using Newtonsoft.Json;

namespace PG.Services.MappingModelsAPI
{
    public class SongAPI
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("artist")]
        public ArtistAPI Artist { get; set; }

        [JsonProperty("genreId")]
        public int GenreId { get; set; }
    }
}
