using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Creator
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tracklsit")]
        public string Tracklist { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
