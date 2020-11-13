using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface ISongService
    {
        Task<SongDTO> Create(SongDTO songDTO);
        Task<IEnumerable<SongDTO>> GetAllSongs();
        Task<IEnumerable<SongDTO>> GetSongsByArtist(int artistId);
        Task<SongDTO> GetSongById(int id);
        Task<SongDTO> Update(int id, SongDTO songDTO);
        Task Delete(int id);
    }
}
