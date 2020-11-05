using System.Collections.Generic;

namespace PG.Services.MappingModelsAPI
{
    public class QueryPlaylistsAPI
    {
        public List<PlaylistLinkAPI> data { get; set; }
        public int total { get; set; }
        public string next { get; set; }
    }
}
