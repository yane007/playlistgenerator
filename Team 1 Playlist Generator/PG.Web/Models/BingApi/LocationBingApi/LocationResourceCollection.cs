using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeezerApiData.Models.BingApi.LocationBingApi
{
    public class LocationResourceCollection
    {
       // [JsonPropertyName("resources")]
        public IEnumerable<LocationResource> LocationResources { get; set; }        
    }
}