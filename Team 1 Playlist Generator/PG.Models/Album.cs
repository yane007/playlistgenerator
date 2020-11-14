using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Album : Entity
    {
        public Album()
        {
            Songs = new List<Song>();
        }

        [MaxLength(500)]
        public string Title { get; set; }

        public string Tracklist { get; set; }

        public IList<Song> Songs { get; set; }
    }
}
