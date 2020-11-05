using PG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.MappingModelsAPI
{
    public class SongAPI
    {
        //public int id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public int duration { get; set; }
        public int rank { get; set; }
        public string preview { get; set; }
        public string type { get; set; }
        public CreatorAPI artist { get; set; }
        public int albumId { get; set; }
        public int genreId { get; set; }
    }
}
