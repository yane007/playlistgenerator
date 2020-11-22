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
                UserId = playlist.UserId,
                Title = playlist.Title,
                Duration = playlist.Duration,
                Rank = playlist.Rank,
                //PixabayImage = playlist.PixabayImage.ToDTO(),
                PlaylistsSongs = playlist.PlaylistsSongs,
            };
        }
        public static Playlist ToEntity(this PlaylistDTO playlistDTO)
        {
            return new Playlist()
            {
                Id = playlistDTO.Id,
                UserId = playlistDTO.UserId,
                Title = playlistDTO.Title,
                Duration = playlistDTO.Duration,
                Rank = playlistDTO.Rank,
                //PixabayImage = playlistDTO.PixabayImage.ToEntity(),
                PlaylistsSongs = playlistDTO.PlaylistsSongs,
            };
        }
    }
}
