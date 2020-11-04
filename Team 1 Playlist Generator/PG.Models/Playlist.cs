using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Playlist
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("is_loved_track")]
        public bool Is_loved_track { get; set; }

        [JsonProperty("collaborative")]
        public bool Collaborative { get; set; }

        [JsonProperty("nb_tracks")]
        public int Nb_tracks { get; set; }

        [JsonProperty("fans")]
        public int Fans { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("share")]
        public string Share { get; set; }

        [JsonProperty("picture")]
        public string picture { get; set; }

        [JsonProperty("picture_small")]
        public string Picture_small { get; set; }

        [JsonProperty("picture_medium")]
        public string Picture_medium { get; set; }

        [JsonProperty("picture_big")]
        public string Picture_big { get; set; }

        [JsonProperty("picture_xl")]
        public string Picture_xl { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        [JsonProperty("tracklist")]
        public string Tracklist { get; set; }

        [JsonProperty("creation_date")]
        public string Creation_date { get; set; }

        [JsonProperty("md5_image")]
        public string Md5_image { get; set; }

        [JsonProperty("creator")]
        public Creator Creator { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tracks")]
        public ICollection<Song> Songs { get; set; } = new List<Song>();


        public int UserId { get; set; }
        public User User { get; set; }


    }
}