using Microsoft.EntityFrameworkCore;
using PG.Data.Context;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Mappers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Services
{
    public class GenreService : IGenreService
    {
        private readonly PGDbContext _context;

        public GenreService(PGDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// This method create a new Genre
        /// </summary>
        /// <param name="genreDTO">GenreDTO</param>
        /// <returns>Returns new Genre</returns>

        public async Task<GenreDTO> Create(GenreDTO genreDTO)
        {
            if (genreDTO == null)
            {
                throw new ArgumentNullException("Null Genre");
            }
            if (genreDTO.Name.Length > 50)
            {
                throw new ArgumentOutOfRangeException("Genre's Name needs to be shorter than 50 characters.");
            }

            var existingGenre = await _context.Genres.FirstOrDefaultAsync(x => x.Name == genreDTO.Name);
            if (existingGenre != null)
            {
                throw new ArgumentException($"Genre with name '{genreDTO.Name}' already exists.");
            }

            var genre = _context.Genres.Add(genreDTO.ToEntity());
            await _context.SaveChangesAsync();

            return genre.Entity.ToDTO();
        }
        /// <summary>
        /// This method delete Genre by it's Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns true if Genre is deleted succesfully</returns>

        public async Task Delete(int id)
        {
            var expectedGenre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (expectedGenre == null)
            {
                throw new ArgumentNullException($"Song with id {id} was not found.");
            }
            if (expectedGenre.IsDeleted)
            {
                throw new ArgumentException($"Song with id {id} is already deleted.");
            }

            expectedGenre.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// This method take's all genres from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GenreDTO>> GetAllGenres()
        {
            return await _context.Genres.Where(x => x.IsDeleted == false)
                                  .Select(x => x.ToDTO())
                                  .ToListAsync();
        }
        /// <summary>
        /// This method take genre by it's Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns genre with given Id</returns>
        public async Task<GenreDTO> GetGenreById(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (genre == null)
            {
                throw new ArgumentNullException($"Genre with id {id} was not found.");
            }

            return genre.ToDTO();
        }
        /// <summary>
        /// This method update genre by Id and change it's name
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="genreDTO">GenreDTO</param>
        /// <returns>Returns updated genre</returns>
        public async Task<GenreDTO> Update(int id, GenreDTO genreDTO)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (genre == null)
            {
                throw new ArgumentNullException($"Genre with id {id} was not found.");
            }

            genre.Name = genreDTO.Name;

            await _context.SaveChangesAsync();

            return genre.ToDTO();
        }
    }
}
