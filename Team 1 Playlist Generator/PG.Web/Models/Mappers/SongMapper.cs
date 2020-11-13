using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models.Mappers
{
    public static class SongMapper
    {
        public static SongViewModel ToViewModel(this SongDTO songDTO)
        {
            return new SongViewModel()
            {
                Title = songDTO.Title,
                Duration = songDTO.Duration,
                Rank = songDTO.Rank,
                Preview = songDTO.Preview,
                ArtistId = songDTO.ArtistId,
                GenreId = songDTO.GenreId,
            };
        }

        public static SongDTO ToDTO(this SongViewModel songViewModel)
        {
            return new SongDTO()
            {
                Title = songViewModel.Title,
                Duration = songViewModel.Duration,
                Rank = songViewModel.Rank,
                Preview = songViewModel.Preview,
                ArtistId = songViewModel.ArtistId,
                GenreId = songViewModel.GenreId,
            };
        }
    }
}
