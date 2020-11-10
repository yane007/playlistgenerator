using PG.Models;
using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models.Mappers
{
    public static class PlaylistMapper
    {
        public static PlaylistViewModel ToViewModel(this PlaylistDTO playlistDTO)
        {
            return new PlaylistViewModel()
            {
                Title = playlistDTO.Title,
                Description = playlistDTO.Description,
                Duration = playlistDTO.Duration,
                Fans = playlistDTO.Fans,
                Link = playlistDTO.Link,
                Share = playlistDTO.Share,
                Picture = playlistDTO.Picture,
                Tracklist = playlistDTO.Tracklist,
                Creation_date = playlistDTO.Creation_date,
                Type = playlistDTO.Type,
                PlaylistsSongs = playlistDTO.PlaylistsSongs,
            };
        }
    }
}
