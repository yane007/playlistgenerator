using PG.Models;
using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IPlaylistService
    {
        /// <summary>
        /// Creates a playlist
        /// </summary>
        /// <param name="playlistDTO">Playlist to create</param>
        Task<Playlist> Create(PlaylistDTO playlistDTO);

        /// <summary>
        /// Gets all playlsits.
        /// </summary>
        Task<IEnumerable<PlaylistDTO>> GetAllPlaylists();

        /// <summary>
        /// Gets all playlists by user's ID.
        /// </summary>
        /// <param name="userId">User's ID.</param>
        Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(string userId);

        /// <summary>
        /// Gets playlist by ID.
        /// </summary>
        /// <param name="id">ID of the playlist.</param>
        Task<PlaylistDTO> GetPlaylistById(int id);

        /// <summary>
        /// Updates a plalist
        /// </summary>
        /// <param name="id">ID of the playlist to update</param>
        /// <param name="playlistDTO">The new data of the playlist</param>
        Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO);

        /// <summary>
        /// Deletes a playlist.
        /// </summary>
        /// <param name="id">ID of the playlist</param>
        Task Delete(int id);

        /// <summary>
        /// Generates a playlist
        /// </summary>
        /// <param name="timeForTrip">Seconds of the trip</param>
        /// <param name="playlistTitle">Playlist's title</param>
        /// <param name="metalPercentagee">Percentage for Metal songs.</param>
        /// <param name="rockPercentagee">Percentage for Rock songs.</param>
        /// <param name="popPercentagee">Percentage for Pop songs.</param>
        /// <param name="topTracks">Allows using top tracks (tracks with s rank higher than 100,000).</param>
        /// <param name="sameArtist">Allows songs from the same artist.</param>
        /// <param name="user">User that creates the playlist</param>
        Task GeneratePlaylist(int tripTime, string playlistName, int metalPercentage, 
            int rockPercentage, int popPercentage, int chalgaPercentage, bool topTracks, bool sameArtist, User user);
    }
}
