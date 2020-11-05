using PG.Services.Contract;
using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PG.Services
{
    public class GenreService : IGenreService
    {
        public Task<GenreDTO> Create(GenreDTO GenreDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GenreDTO>> GetAllGenres()
        {
            throw new NotImplementedException();
        }

        public Task<GenreDTO> GetGenreById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenreDTO> Update(int id, GenreDTO genreDTO)
        {
            throw new NotImplementedException();
        }
    }
}
