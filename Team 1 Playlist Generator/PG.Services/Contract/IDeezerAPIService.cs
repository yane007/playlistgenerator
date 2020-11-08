using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IDeezerAPIService
    {
        Task ExtractSongsFromPlaylists(string genreString);
    }
}
