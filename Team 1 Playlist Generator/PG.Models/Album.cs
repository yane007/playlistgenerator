using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Album : Entity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public int SongId { get; set; }
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }

}
