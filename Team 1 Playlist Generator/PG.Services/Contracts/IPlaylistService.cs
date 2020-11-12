using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IPlaylistService
    {
        Task<PlaylistDTO> Create(PlaylistDTO playlistDTO);
        Task<IEnumerable<PlaylistDTO>> GetAllPlaylists();
        Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(int userId);
        Task<PlaylistDTO> GetPlaylistById(int id);
        Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO);
        Task<bool> Delete(int id);
        Task<int> GeneratePlaylist(PlaylistDTO playlist);
    }
}
