using PG.Services.DTOs;
using System;
using System.Linq;

namespace PG.Web.Models.Mappers
{
    public static class PlaylistMapper
    {
        public static PlaylistViewModel ToViewModel(this PlaylistDTO playlistDTO)
        {
            TimeSpan time = TimeSpan.FromSeconds(playlistDTO.Duration);

            return new PlaylistViewModel()
            {
                Id = playlistDTO.Id,
                UserId = playlistDTO.UserId,
                Title = playlistDTO.Title,
                Duration = playlistDTO.Duration,
                Rank = playlistDTO.Rank,
                DurationInHours = time.ToString(@"hh\:mm\:ss"),
                PixabayImage = playlistDTO.PixabayImage,
                Songs = playlistDTO.Songs.Select(x => x.ToViewModel()).ToList(),
                DeezerIDs = playlistDTO.Songs.Select(x => x.DeezerID).ToList(),
            };
        }

        public static PlaylistDTO ToDTO(this PlaylistViewModel playlistViewModel)
        {
            return new PlaylistDTO()
            {
                Title = playlistViewModel.Title,
                Duration = playlistViewModel.Duration,
                PixabayImage = playlistViewModel.PixabayImage,
            };
        }
    }
}
