using Newtonsoft.Json;
using System.Collections.Generic;

namespace PG.Services.MappingModelsAPI
{
    public class QueryPlaylistsAPI
    {
        //[JsonProperty("data")]
        public List<PlaylistLinkAPI> Data { get; set; }

        //[JsonProperty("total")]
        public int Total { get; set; }

        //[JsonProperty("next")]
        public string Next { get; set; }
    }
}
