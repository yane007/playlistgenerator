using PG.Models;
using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.Mappers
{
    public static class GenreMapper
    {
        public static GenreDTO ToDTO(this Genre genre)
        {
            return new GenreDTO()
            {
                Name = genre.Name
            };
        }
        public static Genre ToEntity(this GenreDTO genreDTO)
        {
            return new Genre()
            {
                Name = genreDTO.Name
            };
        }
    }
}
