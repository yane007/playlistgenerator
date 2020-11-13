using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models.Mappers
{
    public static class GenreMapper
    {
        public static GenreViewModel ToViewModel(this GenreDTO genreDTO)
        {
            return new GenreViewModel()
            {
                Name = genreDTO.Name,
            };
        }

        public static GenreDTO ToDTO(this GenreViewModel genreDTO)
        {
            return new GenreDTO()
            {
                Name = genreDTO.Name,
            };
        }
    }
}
