using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PG.Models
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }
        public string Link { get; set; }
        public int Duration { get; set; }
        public int Rank { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();

    }
}