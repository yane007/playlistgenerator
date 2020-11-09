

using Newtonsoft.Json;

namespace PG.Services.MappingModelsAPI
{
    public class ArtistAPI
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tracklist")]
        public string Tracklist { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
