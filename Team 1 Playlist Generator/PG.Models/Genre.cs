using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Genre : Entity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<PlaylistsGenres> PlaylistsGenres { get; set; }
    }
}
