using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeezerApiData.Models.BingApi
{
    public class TravelResultsCollection
    {
        [JsonPropertyName("results")]
        public IEnumerable<TravelResult> Results { get; set; }
    }
}