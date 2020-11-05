using Newtonsoft.Json;
using PG.Models.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PG.Models
{
    public class Artist : IdAndIsDeleted
    {
        public string Name { get; set; }

        public string Tracklist { get; set; }

        public string Type { get; set; }
    }
}
