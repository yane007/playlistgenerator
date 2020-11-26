using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Album : Entity
    {

        [MaxLength(500)]
        public string Title { get; set; }

        public string Tracklist { get; set; }
    }
}
