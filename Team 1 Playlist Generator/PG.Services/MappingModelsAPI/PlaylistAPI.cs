using Newtonsoft.Json;
using System.Collections.Generic; 

namespace PG.Services.MappingModelsAPI
{
    public class PlaylistAPI
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("data")]
        public List<SongAPI> Data{ get; set; }
    }
}
