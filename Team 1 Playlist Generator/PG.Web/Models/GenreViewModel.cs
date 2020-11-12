using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class GenreViewModel
    {
        public GenreViewModel()
        {

        }

        public GenreViewModel(GenreDTO genre)
        {
            Id = genre.Id;
            Name = genre.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
