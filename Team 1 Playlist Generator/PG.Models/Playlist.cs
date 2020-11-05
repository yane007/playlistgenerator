using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PG.Models
{
    public class Playlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }

        public int Fans { get; set; }

        public string Link { get; set; }

        public string Share { get; set; }

        public string picture { get; set; }

        public string Tracklist { get; set; }

        public string Creation_date { get; set; }

        public Creator Creator { get; set; }

        public string Type { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();


        public int UserId { get; set; }
        public User User { get; set; }


    }
}