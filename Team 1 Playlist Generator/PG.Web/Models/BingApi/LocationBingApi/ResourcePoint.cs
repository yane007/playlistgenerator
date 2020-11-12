using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeezerApiData.Models.BingApi.LocationBingApi
{
    public class ResourcePoint
    {
       // [JsonPropertyName("coordinates")]
        public IEnumerable<decimal> Coordinates { get; set; }
    }
}