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
                Id = playlist.Id,
                Title = playlist.Title,
                Duration = playlist.Duration,
                Picture = playlist.Picture,
                PlaylistsSongs = playlist.PlaylistsSongs,
            };
        }
        public static Playlist ToEntity(this PlaylistDTO playlistDTO)
        {
            return new Playlist()
            {
                Id = playlistDTO.Id,
                Title = playlistDTO.Title,
                Duration = playlistDTO.Duration,
                Picture = playlistDTO.Picture,
                PlaylistsSongs = playlistDTO.PlaylistsSongs,
            };
        }
    }
}
