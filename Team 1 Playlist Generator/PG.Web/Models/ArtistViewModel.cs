using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class ArtistViewModel
    {
        public ArtistViewModel()
        {

        }

        public ArtistViewModel(ArtistDTO artist)
        {
            Id = artist.Id;
            Name = artist.Name;
            Tracklist = artist.Tracklist;
            Type = artist.Type;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Tracklist { get; set; }

        public string Type { get; set; }
    }
}
