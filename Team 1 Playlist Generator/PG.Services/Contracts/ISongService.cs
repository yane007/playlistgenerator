using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface ISongService
    {
        /// <summary>
        /// Creates a song.
        /// </summary>
        /// <param name="songDTO">Song to create</param>
        /// <returns></returns>
        Task<SongDTO> Create(SongDTO songDTO);

        /// <summary>
        /// Gets all songs.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SongDTO>> GetAllSongs();

        /// <summary>
        /// Gets a song by artist's ID.
        /// </summary>
        /// <param name="artistId">Artist's ID</param>
        /// <returns></returns>
        Task<IEnumerable<SongDTO>> GetSongsByArtist(int artistId);

        /// <summary>
        /// Gets song by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SongDTO> GetSongById(int id);

        /// <summary>
        /// Updates a song.
        /// </summary>
        /// <param name="id">Song ID that will be updated</param>
        /// <param name="songDTO">Song data to update</param>
        /// <returns></returns>
        Task<SongDTO> Update(int id, SongDTO songDTO);

        /// <summary>
        /// Deletes a song.
        /// </summary>
        /// <param name="id">Song's ID</param>
        /// <returns></returns>
        Task Delete(int id);
    }
}
