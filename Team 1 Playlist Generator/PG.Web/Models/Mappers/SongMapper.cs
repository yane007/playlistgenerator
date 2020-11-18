using PG.Services.DTOs;
using System;

namespace PG.Web.Models.Mappers
{
    public static class SongMapper
    {
        public static SongViewModel ToViewModel(this SongDTO songDTO)
        {
            TimeSpan time = TimeSpan.FromSeconds(songDTO.Duration);

            return new SongViewModel()
            {
                Id = songDTO.Id,
                Title = songDTO.Title,
                Duration = songDTO.Duration,
                DurationInMinutes = time.ToString(@"mm\:ss"),
                Rank = songDTO.Rank,
                Preview = songDTO.Preview,
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
            };
        }
    }
}
