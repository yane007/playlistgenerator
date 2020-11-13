using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeezerApiData.Models.BingApi.LocationBingApi
{
    public class LocationBingResult
    {
        [JsonPropertyName("resourceSets")]
        public IEnumerable<LocationResourceCollection> LocationResourceCollection { get; set; }
    }
}
