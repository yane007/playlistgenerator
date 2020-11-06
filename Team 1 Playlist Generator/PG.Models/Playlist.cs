using Newtonsoft.Json;
using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Playlist : IdAndIsDeleted
    {

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Title             { get; set; }

        public string Description           { get; set; }

        public int    Duration          { get; set; }

        public int    Fans          { get; set; }

        public string Link          { get; set; }

        public string Share             { get; set; }

        public string Picture           { get; set; }

        public string Tracklist             { get; set; }

        public string Creation_date         { get; set; }
         
        public string Type              { get; set; }

        public ICollection<PlaylistAndSongRelation> PlaylistAndSongRelation { get; set; } = new List<PlaylistAndSongRelation>();

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();


        public int UserId { get; set; }
        public User User { get; set; }


    }
}