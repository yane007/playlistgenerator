using PG.Models;
using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IPlaylistService
    {
        Task<Playlist> Create(PlaylistDTO playlistDTO);

        Task<IEnumerable<PlaylistDTO>> GetAllPlaylists();

        Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(string userId);

        Task<PlaylistDTO> GetPlaylistById(int id);

        Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO);

        Task Delete(int id);

        Task GeneratePlaylist(int tripTime, string playlistName, int metalPercentage, 
            int rockPercentage, int popPercentage, bool topTracks, bool sameArtist, User user);
    }
}
