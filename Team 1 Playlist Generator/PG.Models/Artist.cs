using PG.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Artist : Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Tracklist { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }
    }
}
