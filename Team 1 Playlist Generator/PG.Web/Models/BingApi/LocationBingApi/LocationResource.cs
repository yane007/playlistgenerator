using System.Text.Json.Serialization;

namespace DeezerApiData.Models.BingApi.LocationBingApi
{
    public class LocationResource
    {
       // [JsonPropertyName("point")]
        public ResourcePoint ResourcePoint { get; set; }
    }
}