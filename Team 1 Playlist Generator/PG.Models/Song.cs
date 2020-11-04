using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Song
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("readable")]
        public bool Readable { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_short")]
        public string Title_short { get; set; }

        [JsonProperty("title_version")]
        public string Title_version { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("explicit_lyrics")]
        public bool Explicit_lyrics { get; set; }

        [JsonProperty("explicit_content_lyrics")]
        public int Explicit_content_lyrics { get; set; }

        [JsonProperty("explicit_content_cover")]
        public int Explicit_content_cover { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("md5_image")]
        public string Md5_image { get; set; }

        [JsonProperty("time_add")]
        public int Time_add { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("artist")]
        public Creator Artist { get; set; }


        [JsonProperty("album")]
        public Album Album { get; set; }
        public int AlbumId { get; set; }

        public Playlist Playlist { get; set; }
        public int PlaylistId { get; set; }
    }
}