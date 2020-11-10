using PG.Models;
using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.Mappers
{
    public static class PlaylistMapper
    {
        public static PlaylistDTO ToDTO(this Playlist playlist)
        {
            return new PlaylistDTO()
            {
                Title = playlist.Title,
                Description = playlist.Description,
                Duration = playlist.Duration,
                Fans = playlist.Fans,
                Link = playlist.Link,
                Share = playlist.Share,
                Picture = playlist.Picture,
                Tracklist = playlist.Tracklist,
                Creation_date = playlist.Creation_date,
                Type = playlist.Type,
                PlaylistsSongs = playlist.PlaylistsSongs,
                Id = playlist.Id
            };
        }
        public static Playlist ToModel(this PlaylistDTO playlistDTO)
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
                PlaylistsSongs = playlistDTO.PlaylistsSongs,
                Id = playlistDTO.Id
            };
        }
    }
}
