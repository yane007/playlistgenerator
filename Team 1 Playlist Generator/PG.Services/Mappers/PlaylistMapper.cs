using PG.Models;
using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.Mappers
{
    public static class PlaylistMapper
    {
        public static PlaylistDTO ToDTO(Playlist playlistDTO)
        {
            return new PlaylistDTO()
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
            };
        }
        public static Playlist ToModel(PlaylistDTO playlistDTO)
        {
            return new Playlist()
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
            };
        }
    }
}
