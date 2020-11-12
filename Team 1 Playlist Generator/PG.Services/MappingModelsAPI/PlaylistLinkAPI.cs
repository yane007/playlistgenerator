
using Newtonsoft.Json;

namespace PG.Services.MappingModelsAPI
{
    public class PlaylistLinkAPI
    {
        //[JsonProperty("title")]
        public string Title { get; set; }

        //[JsonProperty("tracklist")]
        public string Tracklist { get; set; }
    }
}
