using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PG.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(30)]
        public string Name { get; set; }
        public string AlbumTrackListURL { get; set; }
    }
}
