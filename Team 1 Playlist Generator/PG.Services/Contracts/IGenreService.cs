using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IGenreService
    {
        /// <summary>
        /// Creates a new Genre
        /// </summary>
        /// <param name="genreDTO">Genre to create</param>
        Task<GenreDTO> Create(GenreDTO GenreDTO);

        /// <summary>
        /// Get Genre by ID
        /// </summary>
        /// <param name="id">Genre's ID</param>
        Task<GenreDTO> GetGenreById(int id);

        /// <summary>
        /// Gets all genres
        /// </summary>
        Task<IEnumerable<GenreDTO>> GetAllGenres();

        /// <summary>
        /// Updates a Genre by ID
        /// </summary>
        /// <param name="id">Genre's ID</param>
        /// <param name="genreDTO">New Genre's data</param>
        Task<GenreDTO> Update(int id, GenreDTO genreDTO);

        /// <summary>
        /// Deletes a Genre by ID
        /// </summary>
        /// <param name="id">Genre's ID</param>
        Task Delete(int id);

        Task SyncGenresAsync();
    }
}