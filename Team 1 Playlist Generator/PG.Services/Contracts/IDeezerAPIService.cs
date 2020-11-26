using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IDeezerAPIService
    {
        /// <summary>
        /// Extract all songs with preview "link".mp3 from all playlists where their title contains "Rock".
        /// Creates new Creator/Artist accordingly to the song's specifications.
        /// </summary>
        Task ExtractSongsFromPlaylists(string genreString);
    }
}
