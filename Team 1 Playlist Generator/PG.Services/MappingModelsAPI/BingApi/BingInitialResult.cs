using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeezerApiData.Models.BingApi
{
    public class BingInitialResult
    {
        [JsonPropertyName("resourceSets")]
        public IEnumerable<BingResource> BingResources { get; set; }
    }
}
