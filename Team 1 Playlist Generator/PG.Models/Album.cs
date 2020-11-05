using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PG.Models
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        public string Title { get; set; }

        public string Cover { get; set; }

        public string Tracklist { get; set; }

        public string Type { get; set; }

        public List<Song> Songs { get; set; }
    }
}
