using Microsoft.EntityFrameworkCore;
using PG.Data.Context;
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
            _context = context;
        }

        public async Task<SongDTO> Create(SongDTO songDTO)
        {
            if (songDTO == null)
            {
                throw new ArgumentNullException("Null Song");
            }
            if (songDTO.Title.Length > 200)
            {
                throw new ArgumentOutOfRangeException("Song's title needs to be shorter than 200 characters.");
            }

            var existingSong = await _context.Songs.FirstOrDefaultAsync(x => x.Title == songDTO.Title);
            if (existingSong != null)
            {
                throw new ArgumentException($"Song with title '{songDTO.Title}' already exists.");
            }

            var song = await _context.Songs.AddAsync(songDTO.ToEntity());
            await _context.SaveChangesAsync();

            return song.Entity.ToDTO();
        }

        public async Task<IEnumerable<SongDTO>> GetAllSongs()
        {
            return await _context.Songs
                                 .Where(x => !x.IsDeleted)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        public async Task<IEnumerable<SongDTO>> GetSongsByArtist(int artistId)
        {
            return await _context.Songs
                                 .Where(x => x.ArtistId == artistId && !x.IsDeleted)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        public async Task<SongDTO> GetSongById(int id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (song == null)
            {
                throw new ArgumentNullException($"Song with id {id} was not found.");
            }

            return song.ToDTO();
        }

        public async Task<SongDTO> Update(int id, SongDTO songDTO)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (song == null)
            {
                throw new ArgumentNullException($"Song with id {id} was not found.");
            }

            song.Title = songDTO.Title;
            song.Duration = songDTO.Duration;
            song.Rank = songDTO.Rank;
            song.Preview = songDTO.Preview;

            if (songDTO.ArtistId != null && songDTO.GenreId != null && songDTO.AlbumId != null)
            {
                song.ArtistId = songDTO.ArtistId.GetValueOrDefault();
                song.GenreId = songDTO.GenreId.GetValueOrDefault();
                song.AlbumId = songDTO.AlbumId.GetValueOrDefault();
            }

            await _context.SaveChangesAsync();

            return song.ToDTO();
        }
        public async Task Delete(int id)
        {
            var expectedSong = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
            if (expectedSong == null)
            {
                throw new ArgumentNullException($"Song with id {id} was not found.");
            }
            if (expectedSong.IsDeleted)
            {
                throw new ArgumentException($"Song with id {id} is already deleted.");
            }

            expectedSong.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
