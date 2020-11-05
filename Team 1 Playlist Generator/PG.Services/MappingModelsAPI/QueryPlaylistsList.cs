using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.MappingModelsAPI
{
    public class QueryPlaylistsList
    {
        public List<LinkForPlaylistData> data { get; set; }
        public int total { get; set; }
        public string next { get; set; }
    }
}
