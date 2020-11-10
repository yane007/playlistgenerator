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
        private readonly PGDbContext _context;

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
                throw new ArgumentException($"Playlist with title '{playlistDTO.Title}' already exists.");
            }

            Playlist playlist = playlistDTO.ToModel();

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return playlistDTO;
        }

        public async Task<IEnumerable<PlaylistDTO>> GetAllPlaylists()
        {
            return await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Where(x => x.IsDeleted == false)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        public async Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(int id)
        {
            return await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Where(x => x.UserId == id && x.IsDeleted == false)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        public async Task<PlaylistDTO> GetPlaylistById(int id)
        {
            var playlist = await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (playlist == null)
            {
                throw new ArgumentNullException($"Playlist with id {id} was not found.");
            }

            return playlist.ToDTO();
        }

        public async Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (playlist == null)
            {
                throw new ArgumentNullException($"Playlist with id {id} was not found.");
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
                throw new ArgumentNullException($"Playlist with id {id} was not found.");
            }
            if (expectedPlaylist.IsDeleted)
            {
                throw new ArgumentException($"Playlist with id {id} is already deleted.");
            }

            expectedPlaylist.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> GeneratePlaylist(PlaylistDTO playlist)
        {
            var playlistToAdd = await _context.Playlists.AddAsync(playlist.ToModel());
            var playlistAdded = playlistToAdd.Entity;

            //Algorithm
            var result = _context.Songs.Take(5);

            foreach (var song in result)
            {
                var relation = new PlaylistsSongs { SongId = song.Id, PlaylistId = playlistAdded.Id };
                playlistAdded.PlaylistsSongs.Add(relation);
                song.PlaylistsSongs.Add(relation);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
