using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface ISongService
    {
        Task<SongDTO> Create(SongDTO songDTO);
        Task<IEnumerable<SongDTO>> GetSongsByUser(int userId);
        Task<SongDTO> GetSongById(int id);
        Task<IEnumerable<SongDTO>> GetAllSongs();
        Task<SongDTO> Update(int id, SongDTO songDTO);
        Task<bool> Delete(int id);
    }
}
