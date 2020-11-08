using System.Collections.Generic;

namespace PG.Services.MappingModelsAPI
{
    public class QueryPlaylistsAPI
    {
        public List<PlaylistLinkAPI> Data { get; set; }
        public int Total { get; set; }
        public string Next { get; set; }
    }
}
