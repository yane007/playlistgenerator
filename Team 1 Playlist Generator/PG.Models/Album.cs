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
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Cover_small { get; set; }
        public string Cover_medium { get; set; }
        public string Cover_big { get; set; }
        public string Cover_xl { get; set; }
        public string Md5_image { get; set; }
        public string Tracklist { get; set; }
        public string Type { get; set; }
    }
}
