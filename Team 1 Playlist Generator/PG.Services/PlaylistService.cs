using PG.Services.Contract;
using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services
{
    public class PlaylistService : IPlaylistService
    {
        public Task<PlaylistDTO> Create(PlaylistDTO playlistDTO)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PlaylistDTO>> GetAllPlaylists()
        {
            throw new System.NotImplementedException();
        }

        public Task<PlaylistDTO> GetPlaylistById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
