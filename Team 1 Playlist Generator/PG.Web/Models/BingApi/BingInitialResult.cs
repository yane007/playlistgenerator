using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeezerApiData.Models.BingApi
{
    public class BingInitialResult
    {
        [JsonPropertyName("resourceSets")]
        public IEnumerable<BingResource> BingResources { get; set; }
    }
}
