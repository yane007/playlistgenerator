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
    public class SongService : ISongService
    {
        private readonly PGDbContext _context;

        public SongService(PGDbContext context)
        {
            this._context = context;
        }


        public async Task<SongDTO> Create(SongDTO songDTO)
        {
            if (songDTO == null)
            {
                throw new ArgumentNullException("Null Playlist");
            }
            if (songDTO.Title.Length > 50)
            {
                throw new ArgumentOutOfRangeException("Song's title needs to be shorter than 50 characters.");
            }

            var existingPlaylist = _context.Songs.FirstOrDefaultAsync(x => x.Title == songDTO.Title);
            if (existingPlaylist != null)
            {
                throw new ArgumentException($"Song with title '{songDTO.Title}' already exists.");
            }

            _context.Songs.Add(songDTO.ToModel());
            await _context.SaveChangesAsync();

            return songDTO;
        }

        public async Task<IEnumerable<SongDTO>> GetAllSongs()
        {
            return await _context.Songs
                                 .Where(x => x.IsDeleted == false)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        public async Task<IEnumerable<SongDTO>> GetSongsByArtist(int artistId)
        {
            return await _context.Songs
                                 .Where(x => x.ArtistId == artistId && x.IsDeleted == false)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        public async Task<SongDTO> GetSongById(int id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (song == null)
            {
                throw new ArgumentNullException($"Song with id {id} was not found.");
            }

            return song.ToDTO();
        }

        public async Task<SongDTO> Update(int id, SongDTO songDTO)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (song == null)
            {
                throw new ArgumentNullException($"Song with id {id} was not found.");
            }

            song.Title = songDTO.Title;
            song.Duration = songDTO.Duration;
            song.Rank = songDTO.Rank;
            song.Preview = songDTO.Preview;
            song.ArtistId = songDTO.ArtistId;
            song.GenreId = songDTO.GenreId;

            await _context.SaveChangesAsync();

            return songDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var expectedSong = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
            if (expectedSong == null)
            {
                throw new ArgumentNullException($"Song with id {id} was not found.");
            }
            if (expectedSong.IsDeleted == true)
            {
                throw new ArgumentException($"Song with id {id} is already deleted.");
            }

            expectedSong.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true; 
        }
    }
}
