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
        Task<IEnumerable<SongDTO>> GetAllSongs();
        Task<IEnumerable<SongDTO>> GetSongsByArtist(int artistId);
        Task<SongDTO> GetSongById(int id);
        Task<SongDTO> Update(int id, SongDTO songDTO);
        Task<bool> Delete(int id);
        Task<bool> AddSongToPlaylisy(int songId, int playlistId);
    }
}
