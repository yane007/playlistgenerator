using System.Collections.Generic; 

namespace PG.Services.MappingModelsAPI
{
    public class PlaylistAPI
    {
        public int Total { get; set; }
        public List<SongAPI> Data{ get; set; }
    }
}
