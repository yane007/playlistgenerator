using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IGenreService
    {
        Task<GenreDTO> Create(GenreDTO GenreDTO);
        Task<GenreDTO> GetGenreById(int id);
        Task<IEnumerable<GenreDTO>> GetAllGenres();
        Task<GenreDTO> Update(int id, GenreDTO genreDTO);
        Task<bool> Delete(int id);
    }
}