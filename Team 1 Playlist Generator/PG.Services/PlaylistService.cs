using Microsoft.EntityFrameworkCore;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Services
{
    public class PlaylistService : IPlaylistService
    {
        private PGDbContext _context;

        public PlaylistService(PGDbContext context)
        {
            _context = context;
        }


        public async Task<PlaylistDTO> Create(PlaylistDTO playlistDTO)
        {
            if (playlistDTO == null)
            {
                throw new ArgumentNullException("Null Playlist");
            }
            if (playlistDTO.Title.Length > 50)
            {
                throw new ArgumentOutOfRangeException("Playlist's title needs to be shorter than 50 characters.");
            }

            var existingPlaylist = _context.Playlists.FirstOrDefaultAsync(x => x.Title == playlistDTO.Title);
            if (existingPlaylist != null)
            {
                throw new ArgumentException($"Playlist with name '{playlistDTO.Title}' already exists.");
            }

            Playlist playlist = playlistDTO.ToModel();

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return playlistDTO;
        }

        public async Task<IEnumerable<PlaylistDTO>> GetAllPlaylists()
        {
            var playlists = await _context.Playlists
                                          .Where(x => x.IsDeleted == false)
                                          .Select(x => x.ToDTO())
                                          .ToListAsync();

            return playlists;
        }

        public async Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(int id)
        {
            var playlists = await _context.Playlists
                                          .Where(x => x.UserId == id && x.IsDeleted == false)
                                          .Select(x => x.ToDTO())
                                          .ToListAsync();

            return playlists;
        }

        public async Task<PlaylistDTO> GetPlaylistById(int id)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (playlist == null)
            {
                throw new ArgumentNullException($"Platlist with id {id} was not found.");
            }

            return playlist.ToDTO();
        }

        public async Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (playlist == null)
            {
                throw new ArgumentNullException($"Platlist with id {id} was not found.");
            }

            playlist.Title = playlistDTO.Title;
            playlist.Description = playlistDTO.Description;
            playlist.Duration = playlistDTO.Duration;
            playlist.Fans = playlistDTO.Fans;
            playlist.Link = playlistDTO.Link;
            playlist.Share = playlistDTO.Share;
            playlist.Picture = playlistDTO.Picture;
            playlist.Tracklist = playlistDTO.Tracklist;
            playlist.Creation_date = playlistDTO.Creation_date;
            playlist.Type = playlistDTO.Type;

            await _context.SaveChangesAsync();

            return playlistDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var expectedPlaylist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id);
            if (expectedPlaylist == null)
            {
                throw new ArgumentNullException($"Platlist with id {id} was not found.");
            }
            if (expectedPlaylist.IsDeleted == true)
            {
                throw new ArgumentException($"Platlist with id {id} is already deleted.");
            }

            expectedPlaylist.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
