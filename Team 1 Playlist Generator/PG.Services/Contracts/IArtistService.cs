using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IArtistService
    {
        /// <summary>
        /// Creates an Artist
        /// </summary>
        /// <param name="artistDTO">Artist to create</param>
        Task<ArtistDTO> Create(ArtistDTO ArtistDTO);

        /// <summary>
        /// Gets an Artist by ID
        /// </summary>
        /// <param name="id">Artist's ID</param>
        Task<ArtistDTO> GetArtistById(int id);

        /// <summary>
        /// Gets all artist
        /// </summary>
        Task<IEnumerable<ArtistDTO>> GetAllArtists();

        /// <summary>
        /// Updates an Artist by ID
        /// </summary>
        /// <param name="id">Artist's ID</param>
        /// <param name="artistDTO">New Artist's data</param>
        Task<ArtistDTO> Update(int id, ArtistDTO artistDTO);

        /// <summary>
        /// Deletes an Artist by ID
        /// </summary>
        /// <param name="id">Artist's ID</param>
        Task Delete(int id);
    }
}
