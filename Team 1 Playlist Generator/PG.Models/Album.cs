using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Album
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("cover")]
        public string Cover { get; set; }

        [JsonProperty("cover_small")]
        public string Cover_small { get; set; }

        [JsonProperty("cover_medium")]
        public string Cover_medium { get; set; }

        [JsonProperty("cover_big")]
        public string Cover_big { get; set; }

        [JsonProperty("Cover_xl")]
        public string Cover_xl { get; set; }

        [JsonProperty("md5_image")]
        public string Md5_image { get; set; }

        [JsonProperty("tracklist")]
        public string Tracklist { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tracks")]
        public string Songs { get; set; }
    }
}
