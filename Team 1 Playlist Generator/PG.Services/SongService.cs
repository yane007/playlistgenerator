using PG.Services.Contract;
using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services
{
    public class SongService : ISongService
    {
        public Task<SongDTO> Create(SongDTO songDTO)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SongDTO>> GetAllSongs()
        {
            throw new System.NotImplementedException();
        }

        public Task<SongDTO> GetSongById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SongDTO>> GetSongsByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<SongDTO> Update(int id, SongDTO songDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
