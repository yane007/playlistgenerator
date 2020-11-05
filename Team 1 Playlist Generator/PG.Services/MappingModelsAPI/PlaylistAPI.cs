using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.MappingModelsAPI
{
    public class PlaylistAPI
    {
        public int total { get; set; }
        public List<SongAPI> data{ get; set; }
    }
}
