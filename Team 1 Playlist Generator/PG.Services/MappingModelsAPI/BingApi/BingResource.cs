using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeezerApiData.Models.BingApi
{
    public class BingResource
    {
        [JsonPropertyName("resources")]

        public IEnumerable<TravelResultsCollection> TravelResultsCollections { get; set; }
    }
}